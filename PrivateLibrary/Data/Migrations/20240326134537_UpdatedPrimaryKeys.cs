using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrivateLibrary.Data.Migrations
{
    public partial class UpdatedPrimaryKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "516d66aa-715d-484d-baba-ffebc3719169", "AQAAAAEAACcQAAAAENwkYmpsrCQdEeHr8u/8yHS7GS1wj4pdD709GKe/j4FUTQNFEqrLVMCGp4Zai9o0MQ==", "999e6dd3-7a29-49f5-9aca-5c7dc0a7dc56" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9177d797-43b1-4157-8d8c-57ba64b0aa43", "AQAAAAEAACcQAAAAEPxYpTrmwfTqCViZKWSzNJY4bfDzqdI3Sd91CV47CPFrAsUy6ZYAa3YK2D7rctaV3g==", "fdb8b083-9529-4cf9-aa18-e75bf6925a0f" });
        }
    }
}
