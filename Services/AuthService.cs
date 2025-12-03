// Services/AuthService.cs
using FoodSystem.Data;
using FoodSystem.DTOs;
using FoodSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodSystem.Services
{

    public interface IAuthService
    {
        Task<AuthResult> RegisterRiderAsync(RiderRegisterDto riderRegisterDto);
        Task<AuthResult> RegisterAsync(RegisterDto registerDto, string role = "User");
        Task<AuthResult> LoginAsync(LoginDto loginDto);
        Task<bool> AssignRoleAsync(string email, string role);
        Task<bool> RemoveUserAsync(string email);
        Task<List<UserResponseDto>> GetAllUsersAsync();
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    }

    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<AuthResult> RegisterAsync(RegisterDto registerDto, string role = "User")
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return new AuthResult { Success = false, Message = "User already exists!" };
            }

            var newUser = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                DeliveryAddress = registerDto.DeliveryAddress,
                Country = registerDto.Country
            };

            var createResult = await _userManager.CreateAsync(newUser, registerDto.Password);
            if (!createResult.Succeeded)
            {
                return new AuthResult { Success = false, Message = "User creation failed!", Errors = createResult.Errors.Select(e => e.Description) };
            }

            // Ensure role exists
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            // Assign role to user
            await _userManager.AddToRoleAsync(newUser, role);

            var token = GenerateJwtToken(newUser, role);
            return new AuthResult { Success = true, Token = token, Message = "User registered successfully!" };
        }

        public async Task<AuthResult> RegisterRiderAsync(RiderRegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return new AuthResult { Success = false, Message = "User already exists" };

            // create Identity user (User)
            var newUser = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Country = dto.Country,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(newUser, dto.Password);
            if (!createResult.Succeeded)
                return new AuthResult { Success = false, Message = "User creation failed", Errors = createResult.Errors.Select(e => e.Description) };

            // ensure role exists
            if (!await _roleManager.RoleExistsAsync("Rider"))
                await _roleManager.CreateAsync(new IdentityRole("Rider"));

            await _userManager.AddToRoleAsync(newUser, "Rider");

            // create Rider profile
            var riderProfile = new Rider
            {
                UserId = newUser.Id,
                MotorcycleModel = dto.MotorcycleModel,
                PlateNumber = dto.PlateNumber,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Riders.Add(riderProfile);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(newUser, "Rider");

            return new AuthResult { Success = true, Token = token, Message = "Rider registered successfully!", Role = "Rider" };
        }

        public async Task<AuthResult> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return new AuthResult { Success = false, Message = "Invalid credentials!" };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";

            var token = GenerateJwtToken(user, role);
            return new AuthResult { Success = true, Token = token, Message = "Login successful!", Role = role };
        }

        public async Task<bool> AssignRoleAsync(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            var result = await _userManager.AddToRoleAsync(user, role);

            return result.Succeeded;
        }

        public async Task<bool> RemoveUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            var userDtos = new List<UserResponseDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new UserResponseDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    DeliveryAddress = user.DeliveryAddress,
                    Country = user.Country,
                    Role = roles.FirstOrDefault() ?? "User"
                });
            }

            return userDtos;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null) return false;

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            return result.Succeeded;
        }

        private string GenerateJwtToken(User user, string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role),
                new Claim("fullName", user.FullName)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(Convert.ToDouble(jwtSettings["ExpirationInDays"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class AuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}