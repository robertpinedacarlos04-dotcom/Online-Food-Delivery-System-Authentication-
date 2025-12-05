using FoodSystem.Data;
using FoodSystem.DTOs;
using FoodSystem.Models;
using FoodSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AdminController(
            IAuthService authService,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _authService = authService;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpPost("create-rider")]
        public async Task<IActionResult> CreateRider([FromBody] RiderRegisterDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (dto.Password != dto.ConfirmPassword) return BadRequest("Passwords do not match.");

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null) return BadRequest(new { Message = "A user with this email already exists." });

            var existingRider = await _context.Riders.FirstOrDefaultAsync(r => r.PlateNumber == dto.PlateNumber);
            if (existingRider != null) return BadRequest(new { Message = "This Plate-number is already registered." });

            if (dto.PhoneNumber.Length > 15)
                return BadRequest(new { Message = "This Phone-number exceeds the 15 character limit." });

            if (dto.Password.Length < 8)
                return BadRequest(new { Message = "Password must atleast 8." });

            var user = new User
            {
                FullName = dto.FullName,
                UserName = dto.Email,
                Email = dto.Email,
                DeliveryAddress = "N/A",
                PhoneNumber = dto.PhoneNumber,
                Country = dto.Country,         
                EmailConfirmed = true
            };

            var createUserResult = await _userManager.CreateAsync(user, dto.Password);
            if (!createUserResult.Succeeded)
                return BadRequest(createUserResult.Errors);

            if (!await _roleManager.RoleExistsAsync("Rider"))
                await _roleManager.CreateAsync(new IdentityRole("Rider"));

            await _userManager.AddToRoleAsync(user, "Rider");

            var riderProfile = new Rider
            {
                UserId = user.Id,
                Fullname = dto.FullName,
                Email = dto.Email,
                Country = dto.Country,
                MotorcycleModel = dto.MotorcycleModel,
                PlateNumber = dto.PlateNumber,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Riders.Add(riderProfile);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Rider created successfully", RiderEmail = user.Email });
        }

        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto, "Admin");
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto assignRoleDto)
        {
            var result = await _authService.AssignRoleAsync(assignRoleDto.Email, assignRoleDto.Role);

            if (!result)
                return BadRequest(new { Message = "Failed to assign role" });

            return Ok(new { Message = "Role assigned successfully" });
        }

        [HttpDelete("remove-user/{email}")]
        public async Task<IActionResult> RemoveUser(string email)
        {
            var result = await _authService.RemoveUserAsync(email);

            if (!result)
                return BadRequest(new { Message = "Failed to remove user" });

            return Ok(new { Message = "User removed successfully" });
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users);
        }
    }

    public class AssignRoleDto
    {
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
