using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Proje.Migrations
{
    /// <inheritdoc />
    public partial class WebTaban21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OnayDurumu",
                table: "Randevular",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnayDurumu",
                table: "Randevular");
        }
    }
}
