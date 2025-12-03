using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodSystem.Migrations
{
    /// <inheritdoc />
    public partial class CreateRiderProfileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotorcycleModel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlateNumber",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Riders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MotorcycleModel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PlateNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Riders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Riders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "4b2eee6e-1a3c-470b-bd34-4341be447734");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1bbdd26-2d62-4ecf-9135-f5449f71ffb4", "AQAAAAIAAYagAAAAELjZJbxE5gv+/nn2AgKklmw9lB1gylOdZa5HjeGC5n7eb7TFNU8KzbGnMjCwFr6zkA==", "7a6dc77e-e9f3-49cc-b182-83d3c1c45ea4" });

            migrationBuilder.CreateIndex(
                name: "IX_Riders_UserId",
                table: "Riders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Riders");

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
    }
}
