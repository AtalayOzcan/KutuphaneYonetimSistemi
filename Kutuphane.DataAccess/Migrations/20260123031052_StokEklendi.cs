using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kutuphane.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class StokEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "stok",
                table: "Kitaplar",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "stok",
                table: "Kitaplar");
        }
    }
}
