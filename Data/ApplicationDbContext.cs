// Data/ApplicationDbContext.cs
using FoodSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Rider> Riders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Rider>()
                .HasIndex(r => r.PlateNumber)
                .IsUnique();

            base.OnModelCreating(builder);

            // --------- SEED SUPER ADMIN ROLE ---------
            string superAdminRoleId = "1";
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = superAdminRoleId,
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });

            // --------- SEED SUPER ADMIN USER ---------
            string superAdminUserId = "1";
            var user = new User
            {
                Id = superAdminUserId,
                UserName = "superadmin@system.com",
                NormalizedUserName = "SUPERADMIN@SYSTEM.COM",
                Email = "superadmin@system.com",
                NormalizedEmail = "SUPERADMIN@SYSTEM.COM",
                EmailConfirmed = true,

                FullName = "Super Admin",
                Country = "Philippines",
                DeliveryAddress = "System HQ",
                PhoneNumber = "0000000000",

                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),

                CreatedAt = DateTime.Parse("2025-01-01"),
                UpdatedAt = DateTime.Parse("2025-01-01")
            };

            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, "SuperAdmin123!");

            builder.Entity<User>().HasData(user);

            // --------- ASSIGN ROLE TO USER ---------
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = superAdminRoleId,
                UserId = superAdminUserId
            });
        }
    }
}