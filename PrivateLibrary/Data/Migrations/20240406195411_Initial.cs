using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrivateLibrary.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsTaken = table.Column<bool>(type: "bit", nullable: false),
                    CostPerDay = table.Column<double>(type: "float", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EGN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAccountActive = table.Column<bool>(type: "bit", nullable: false),
                    LeaveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Readers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TakenBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfTaking = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfReturn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ReaderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakenBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TakenBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TakenBooks_Readers_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Readers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dea12856-c198-4129-b3f3-b893d8395082", 0, "6ed4c7d2-f1ed-4f11-9bea-3490e8f07d92", "ApplicationUser", "admin@gmail.com", false, "Ivan", "Ivanov", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEOFq4AdD/n8vem+DC1e1N7VgBtd8Y3ytIOmus7RVVMewiJiNsBLk5+ylj9K8XX5Vng==", null, false, "ab6aa058-49b3-429d-888e-345349e48de9", false, "Admin" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CostPerDay", "ISBN", "Image", "IsTaken", "Title" },
                values: new object[,]
                {
                    { 1, "Иван Вазов", 4.0, "9786192510244", "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/p/o/19071533ecfae7e0bc6c0df761f2b77a/pod-igoto-helikon-chervena-koritsa-30.jpg", false, "Под игото" },
                    { 2, "Димитър Димов", 5.0, "9789547385289", "https://i4.helikon.bg/products/4697/14/144697/3613030_b.jpg", false, "Тютюн" },
                    { 3, "Слави Панайотов", 7.0, " 9786197511017", "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/t/o/e563b1cc68b7a08799821bf41c795b65/top-misteriite-na-balgariya-30.jpg", false, "Топ мистериите на България" },
                    { 4, "Димитър Талев", 2.0, "9789549232813", "https://hermesbooks.bg/media/catalog/product/cache/e533a3e3438c08fe7c51cedd0cbec189/j/e/jelezniat_svetilnik_hrm_2_20200901160342.jpg", false, "Железният светилник" },
                    { 5, "Николай Хайтов", 5.0, " 9789540904382", "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/d/i/705d5f90e5fb5ee3725b8922b76073b7/sachineniya-v-17-toma---tom-2--divi-razkazi-31.jpg", false, "Диви Разкази" },
                    { 6, "Ф. Скот Фицджералд", 8.0, "9789542819387", "https://www.ciela.com/media/catalog/product/cache/332cf88b637d37883ec9cea105be873e/g/e/getsbi-koritsa.jpg", false, "Великият Гетсби" },
                    { 7, "Марк Аврелий", 5.0, "9789542838654", "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/m/a/770200d2b28cce79f22d92eb4b8c13ba/mark-avreliy--kam-sebe-si-luksozno-izdanie-30.jpg", false, "Към себе си" },
                    { 8, "Фьодор М. Достоевски", 9.0, "9789540907680", "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/p/r/fe0c5f747aa7d7feab7a3dd45388a570/prestaplenie-i-nakazanie-30.jpg", false, "Престъпление и наказание" },
                    { 9, "Джордж Оруел", 6.0, "9789542833734", "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/1/9/4a0dca141d62410d50fb2615ff15ad73/1984-siela-30.jpg", false, "1984" },
                    { 10, "Джордж Оруел", 5.0, "9789542833703", "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/f/e/ee665f8b54103f6b17870252d8ac34ff/fermata-na-zhivotnite-siela-30.jpg", false, "Фермата на животните" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Readers_UserId",
                table: "Readers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TakenBooks_BookId",
                table: "TakenBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_TakenBooks_ReaderId",
                table: "TakenBooks",
                column: "ReaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "TakenBooks");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Readers");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
