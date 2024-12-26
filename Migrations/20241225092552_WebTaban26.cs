using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Proje.Migrations
{
    /// <inheritdoc />
    public partial class WebTaban26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IslemlerIslemID",
                table: "Randevular",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_CalisanID",
                table: "Randevular",
                column: "CalisanID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_IslemlerIslemID",
                table: "Randevular",
                column: "IslemlerIslemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanID",
                table: "Randevular",
                column: "CalisanID",
                principalTable: "Calisanlar",
                principalColumn: "CalisanID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Islemler_IslemlerIslemID",
                table: "Randevular",
                column: "IslemlerIslemID",
                principalTable: "Islemler",
                principalColumn: "IslemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanID",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Islemler_IslemlerIslemID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_CalisanID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_IslemlerIslemID",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "IslemlerIslemID",
                table: "Randevular");
        }
    }
}
