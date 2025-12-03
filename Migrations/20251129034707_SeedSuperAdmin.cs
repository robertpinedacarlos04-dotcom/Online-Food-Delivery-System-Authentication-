using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodSystem.Migrations
{
    /// <inheritdoc />
    public partial class SeedSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", "8831306b-efed-4a51-833e-083a7843e01d", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Country", "CreatedAt", "DeliveryAddress", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[] { "1", 0, "997cc62c-e6a0-462d-ab1b-53374faccd2a", "Philippines", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System HQ", "superadmin@system.com", true, "Super Admin", false, null, "SUPERADMIN@SYSTEM.COM", "SUPERADMIN@SYSTEM.COM", "AQAAAAIAAYagAAAAELNxb4e6cjljLFwo5bmqvgCg2zPyD0VwSuwNnFUkGhHb+me3OUiaB647BJBaObu0/w==", "0000000000", false, "f29d1aef-7d3a-49af-8588-117490ca5982", false, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@system.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");
        }
    }
}
