using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodSystem.Migrations
{
    /// <inheritdoc />
    public partial class AspNetRider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "9b44a07d-4efb-4b67-b8ed-bcf55a0d270a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b6d4c679-8e7a-4675-94ac-7f602ed5d06e", "AQAAAAIAAYagAAAAEL/kzFeNb8rRS8mx8wriLj4Vsg6ep2tWmnZqbMGGLkgtsipEaiDUOcioISfvvjRxrg==", "742948a5-d702-4a78-a79f-69b03cd15a41" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "b7ee580d-58a4-40ed-8c49-5c8edae6fb1d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b9b562c-53ab-4a06-adb8-00695be85617", "AQAAAAIAAYagAAAAEI7HsM2JRKONpkoQgw136RloqBHq6Knhj+O8EDUP1UMtOv/d1SOLlF3zMCFDh9QGAA==", "8ebf2093-e4b5-4521-b02b-07a00f16d1d8" });
        }
    }
}
