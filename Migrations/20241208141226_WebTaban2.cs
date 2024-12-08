using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Proje.Migrations
{
    /// <inheritdoc />
    public partial class WebTaban2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UzmanlikAlani",
                table: "Calisanlar");

            migrationBuilder.AddColumn<string>(
                name: "Aciklama",
                table: "Calisanlar",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IslemID",
                table: "Calisanlar",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "islemlerIslemID",
                table: "Calisanlar",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_islemlerIslemID",
                table: "Calisanlar",
                column: "islemlerIslemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Calisanlar_Islemler_islemlerIslemID",
                table: "Calisanlar",
                column: "islemlerIslemID",
                principalTable: "Islemler",
                principalColumn: "IslemID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_Islemler_islemlerIslemID",
                table: "Calisanlar");

            migrationBuilder.DropIndex(
                name: "IX_Calisanlar_islemlerIslemID",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "Aciklama",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "IslemID",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "islemlerIslemID",
                table: "Calisanlar");

            migrationBuilder.AddColumn<string>(
                name: "UzmanlikAlani",
                table: "Calisanlar",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
