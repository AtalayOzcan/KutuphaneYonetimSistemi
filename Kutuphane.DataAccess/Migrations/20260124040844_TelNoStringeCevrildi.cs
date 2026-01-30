using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kutuphane.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TelNoStringeCevrildi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "telefon_numarasi",
                table: "Uyeler",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "telefon_numarasi",
                table: "Uyeler",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(11)",
                oldMaxLength: 11);
        }
    }
}
