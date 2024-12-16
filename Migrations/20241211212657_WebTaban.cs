using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Proje.Migrations
{
    /// <inheritdoc />
    public partial class WebTaban : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdminEmail = table.Column<string>(type: "TEXT", nullable: true),
                    AdminSifre = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Calisanlar",
                columns: table => new
                {
                    CalisanID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CalisanAd = table.Column<string>(type: "TEXT", nullable: false),
                    CalisanSoyad = table.Column<string>(type: "TEXT", nullable: false),
                    CalisanTelefon = table.Column<string>(type: "TEXT", nullable: false),
                    CalisanEmail = table.Column<string>(type: "TEXT", nullable: false),
                    Aciklama = table.Column<string>(type: "TEXT", nullable: false),
                    IslemID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlar", x => x.CalisanID);
                });

            migrationBuilder.CreateTable(
                name: "CalismaSaatleri",
                columns: table => new
                {
                    CalismaMesaiID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CalisanID = table.Column<int>(type: "INTEGER", nullable: false),
                    Gun = table.Column<string>(type: "TEXT", nullable: true),
                    SaatBaslangic = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    SaatBitis = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalismaSaatleri", x => x.CalismaMesaiID);
                });

            migrationBuilder.CreateTable(
                name: "Islemler",
                columns: table => new
                {
                    IslemID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IslemAdi = table.Column<string>(type: "TEXT", nullable: false),
                    Ucret = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Islemler", x => x.IslemID);
                });

            migrationBuilder.CreateTable(
                name: "Musteri",
                columns: table => new
                {
                    MUsteriID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MUsteriAd = table.Column<string>(type: "TEXT", nullable: false),
                    MUsteriSoyad = table.Column<string>(type: "TEXT", nullable: false),
                    MUsteriTelefon = table.Column<string>(type: "TEXT", nullable: false),
                    MUsteriEmail = table.Column<string>(type: "TEXT", nullable: false),
                    MUsteriSifre = table.Column<string>(type: "TEXT", nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteri", x => x.MUsteriID);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    RandevuID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MusteriID = table.Column<int>(type: "INTEGER", nullable: false),
                    CalisanID = table.Column<int>(type: "INTEGER", nullable: false),
                    IslemID = table.Column<int>(type: "INTEGER", nullable: false),
                    Tarih = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Saat = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.RandevuID);
                });

            migrationBuilder.CreateTable(
                name: "CalisanIslemler",
                columns: table => new
                {
                    calisansCalisanID = table.Column<int>(type: "INTEGER", nullable: false),
                    islemlersIslemID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanIslemler", x => new { x.calisansCalisanID, x.islemlersIslemID });
                    table.ForeignKey(
                        name: "FK_CalisanIslemler_Calisanlar_calisansCalisanID",
                        column: x => x.calisansCalisanID,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalisanIslemler_Islemler_islemlersIslemID",
                        column: x => x.islemlersIslemID,
                        principalTable: "Islemler",
                        principalColumn: "IslemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalisanIslemler_islemlersIslemID",
                table: "CalisanIslemler",
                column: "islemlersIslemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "CalisanIslemler");

            migrationBuilder.DropTable(
                name: "CalismaSaatleri");

            migrationBuilder.DropTable(
                name: "Musteri");

            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "Calisanlar");

            migrationBuilder.DropTable(
                name: "Islemler");
        }
    }
}
