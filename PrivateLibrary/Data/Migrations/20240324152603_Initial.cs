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
                name: "EGN",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAccountActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LeaveDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
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
                        name: "FK_TakenBooks_AspNetUsers_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TakenBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dea12856-c198-4129-b3f3-b893d8395082", 0, "ef92811d-4ae4-4d25-8271-70463b77908d", "ApplicationUser", "admin@gmail.com", false, "Ivan", "Ivanov", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEEjzCoAMegiE2A+8y7Sl3Iu5k93gsXCdBKMWPcGpM7qaYsQ45fkAPuTByYwtgX935g==", null, false, "3e8d8e63-af0b-4809-a99d-40e84f1a891a", false, "Admin" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CostPerDay", "ISBN", "Image", "IsTaken", "Title" },
                values: new object[,]
                {
                    { 1, "Иван Вазов", 4.0, "9786192510244", "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/p/o/19071533ecfae7e0bc6c0df761f2b77a/pod-igoto-helikon-chervena-koritsa-30.jpg", false, "Под игото" },
                    { 2, "Кристина Кръстева", 5.0, "9789543985289", "https://i2.helikon.bg/products/8656/20/208656/208656_b.jpg?t=1711110871", false, "Бойко, който винаги се завръща" },
                    { 3, "Слави Панайотов", 7.0, " 9786197511017", "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/t/o/e563b1cc68b7a08799821bf41c795b65/top-misteriite-na-balgariya-30.jpg", false, "Топ мистериите на България" },
                    { 4, "Сергей Станишев", 2.0, "9789549232813", "https://knizhen-pazar.net/books/115/11511/1151187.jpg?size=23008", false, "Защото сме социалисти" },
                    { 5, "Николай Хайтов", 5.0, " 9789540904382", "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/d/i/705d5f90e5fb5ee3725b8922b76073b7/sachineniya-v-17-toma---tom-2--divi-razkazi-31.jpg", false, "Диви Разкази" },
                    { 6, "Слави Трифонов", 8.0, "9789542819387", "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/z/a/097e7a91150b40cf3a93c94e12dc282a/za-men-e-chest-31.jpg", false, "За мен е чест" },
                    { 7, "Марк Аврелий", 5.0, "9789542838654", "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/m/a/770200d2b28cce79f22d92eb4b8c13ba/mark-avreliy--kam-sebe-si-luksozno-izdanie-30.jpg", false, "Към себе си" },
                    { 8, "Фьодор М. Достоевски", 9.0, "9789540907680", "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/p/r/fe0c5f747aa7d7feab7a3dd45388a570/prestaplenie-i-nakazanie-30.jpg", false, "Престъпление и наказание" },
                    { 9, "Джордж Оруел", 6.0, "9789542833734", "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/1/9/4a0dca141d62410d50fb2615ff15ad73/1984-siela-30.jpg", false, "1984" },
                    { 10, "Джордж Оруел", 5.0, "9789542833703", "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/f/e/ee665f8b54103f6b17870252d8ac34ff/fermata-na-zhivotnite-siela-30.jpg", false, "Фермата на животните" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TakenBooks_BookId",
                table: "TakenBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_TakenBooks_ReaderId",
                table: "TakenBooks",
                column: "ReaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TakenBooks");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EGN",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAccountActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LeaveDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");
        }
    }
}
