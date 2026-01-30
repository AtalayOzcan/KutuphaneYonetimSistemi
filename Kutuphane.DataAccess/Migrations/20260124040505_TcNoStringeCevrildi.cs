using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kutuphane.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TcNoStringeCevrildi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "tc_no",
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
                name: "tc_no",
                table: "Uyeler",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(11)",
                oldMaxLength: 11);
        }
    }
}
