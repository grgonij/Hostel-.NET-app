using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hostel.MVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table => table.PrimaryKey("PK_AspNetRoles", x => x.Id));

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_AspNetUsers", x => x.Id));

            migrationBuilder.CreateTable(
                name: "Lezaji",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    BrojSobe = table.Column<int>(type: "INTEGER", nullable: false),
                    CijenaPoNoci = table.Column<decimal>(type: "TEXT", nullable: false),
                    Dostupan = table.Column<bool>(type: "INTEGER", nullable: false),
                    TipSobe = table.Column<int>(type: "INTEGER", nullable: false),
                    Opis = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    DatumDodavanja = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Lezaji", x => x.Id));

            migrationBuilder.CreateTable(
                name: "Rezervacije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                    ImeGosta = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    DatumPrijave = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DatumOdjave = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Napomena = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    LezajId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Lezaji_LezajId",
                        column: x => x.LezajId,
                        principalTable: "Lezaji",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Lezaji",
                columns: new[] { "Id", "BrojSobe", "CijenaPoNoci", "DatumDodavanja", "Dostupan", "Naziv", "Opis", "TipSobe" },
                values: new object[,]
                {
                    { 1, 101, 15m, new DateTime(2024, 1, 1), true, "Ležaj A1", "Zajednička soba, 6 kreveta.", 0 },
                    { 2, 203, 25m, new DateTime(2024, 1, 15), true, "Ležaj B3", "Privatna soba.", 1 },
                    { 3, 305, 18m, new DateTime(2024, 2, 1), false, "Ležaj C2", "Mješovita soba, 4 kreveta.", 2 }
                });

            migrationBuilder.CreateIndex(name: "IX_Rezervacije_LezajId", table: "Rezervacije", column: "LezajId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Rezervacije");
            migrationBuilder.DropTable(name: "Lezaji");
            migrationBuilder.DropTable(name: "AspNetRoles");
            migrationBuilder.DropTable(name: "AspNetUsers");
        }
    }
}
