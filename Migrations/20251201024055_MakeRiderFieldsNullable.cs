using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodSystem.Migrations
{
    /// <inheritdoc />
    public partial class MakeRiderFieldsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MotorcycleModel",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlateNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "8ae741f2-6125-46de-8632-8e830302a842");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "MotorcycleModel", "PasswordHash", "PlateNumber", "SecurityStamp" },
                values: new object[] { "f607c7bc-9cce-4fe0-8d45-61d7c0ae35b0", null, "AQAAAAIAAYagAAAAEOhRHeM9p8Qh3ZYZXwdjBpl2/Wdg3f9TZWrCU4cc34lOpUZHA2o+5Dywo1F4w73C+w==", null, "0c1b730b-14e5-4b86-af96-58f97024bf70" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotorcycleModel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlateNumber",
                table: "AspNetUsers");

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
    }
}
