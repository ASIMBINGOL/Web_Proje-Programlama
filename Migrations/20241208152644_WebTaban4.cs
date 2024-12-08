using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Proje.Migrations
{
    /// <inheritdoc />
    public partial class WebTaban4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_Islemler_islemlerIslemID",
                table: "Calisanlar");

            migrationBuilder.DropTable(
                name: "CalisanIslemleri");

            migrationBuilder.DropIndex(
                name: "IX_Calisanlar_islemlerIslemID",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "islemlerIslemID",
                table: "Calisanlar");

            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "Calisanlar",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "Calisanlar",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "islemlerIslemID",
                table: "Calisanlar",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CalisanIslemleri",
                columns: table => new
                {
                    CalisanIslemID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CalisanID = table.Column<int>(type: "INTEGER", nullable: false),
                    IslemID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanIslemleri", x => x.CalisanIslemID);
                });

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
    }
}
