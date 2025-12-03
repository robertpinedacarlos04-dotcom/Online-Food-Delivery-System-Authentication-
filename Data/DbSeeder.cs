using FoodSystem.Models;
using FoodSystem.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FoodSystem.Data
{
    public class DbSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SuperAdminSettings _settings;

        public DbSeeder(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<SuperAdminSettings> options)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _settings = options.Value;
        }

        public async Task SeedSuperAdminAsync()
        {
            Console.WriteLine("---- Running SuperAdmin Seeder ----");

            // 1. Create Role
            if (!await _roleManager.RoleExistsAsync("SuperAdmin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                Console.WriteLine("Created SuperAdmin role");
            }

            // 2. Check if user exists
            var superAdmin = await _userManager.FindByEmailAsync(_settings.Email);
            if (superAdmin != null)
            {
                Console.WriteLine("SuperAdmin already exists");
                return;
            }

            // 3. Create user
            var user = new User
            {
                UserName = _settings.Email,
                Email = _settings.Email,
                FullName = _settings.FullName,
                Country = _settings.Country,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, _settings.Password);

            if (!result.Succeeded)
            {
                Console.WriteLine("Failed to create SuperAdmin user:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(" - " + error.Description);
                }
                return;
            }

            Console.WriteLine("SuperAdmin user created successfully");

            // 4. Assign role
            await _userManager.AddToRoleAsync(user, "SuperAdmin");

            Console.WriteLine("SuperAdmin role assigned.");
        }
    }
}
