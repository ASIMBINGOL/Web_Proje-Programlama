using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Proje.Migrations
{
    /// <inheritdoc />
    public partial class WebTaban23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CalismaSaatleri_CalisanID",
                table: "CalismaSaatleri",
                column: "CalisanID");

            migrationBuilder.AddForeignKey(
                name: "FK_CalismaSaatleri_Calisanlar_CalisanID",
                table: "CalismaSaatleri",
                column: "CalisanID",
                principalTable: "Calisanlar",
                principalColumn: "CalisanID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalismaSaatleri_Calisanlar_CalisanID",
                table: "CalismaSaatleri");

            migrationBuilder.DropIndex(
                name: "IX_CalismaSaatleri_CalisanID",
                table: "CalismaSaatleri");
        }
    }
}
