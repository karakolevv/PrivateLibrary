using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrivateLibrary.Data.Migrations
{
    public partial class AddIdToTakeBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "TakenBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "224cb920-05c1-4511-b049-9700d646c3f8", "AQAAAAEAACcQAAAAEMMlJdYDdreDl53weBjNMw72GagCnLLVdi93NymwjecCiXJ1RyUaX5IxUGim5SvUPQ==", "9f85422b-c4e8-4bda-baf1-8ccaeb68f238" });

            migrationBuilder.CreateIndex(
                name: "IX_TakenBooks_BookId",
                table: "TakenBooks",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_TakenBooks_Books_BookId",
                table: "TakenBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakenBooks_Books_BookId",
                table: "TakenBooks");

            migrationBuilder.DropIndex(
                name: "IX_TakenBooks_BookId",
                table: "TakenBooks");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "TakenBooks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5eddffc6-fdf4-4511-94e4-ede5426efe99", "AQAAAAEAACcQAAAAEKlWOsyyG0bCGZ+oAbRL2Hi0j1yQKsb67kLIqLu9uIMMV0f3SWjtoAuIleaVoB+UDQ==", "29926465-ba53-47c2-9ce5-4b6b8bd5bd6d" });
        }
    }
}
