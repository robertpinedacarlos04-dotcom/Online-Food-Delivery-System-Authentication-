using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodSystem.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "058dd4f1-2f92-4a2f-8007-c50b7acb1511");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cc09f21f-642a-436b-8fbd-82e4166f34df", "AQAAAAIAAYagAAAAELXFCJSJztWJDESHPjVVr8OLu6G36ktDOVv4sI2nutFbmV5XdRl9C/5aEB4Z/hWHlw==", "d0f599ca-8da9-4930-a005-41eae0b8ac2a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "8831306b-efed-4a51-833e-083a7843e01d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "997cc62c-e6a0-462d-ab1b-53374faccd2a", "AQAAAAIAAYagAAAAELNxb4e6cjljLFwo5bmqvgCg2zPyD0VwSuwNnFUkGhHb+me3OUiaB647BJBaObu0/w==", "f29d1aef-7d3a-49af-8588-117490ca5982" });
        }
    }
}
