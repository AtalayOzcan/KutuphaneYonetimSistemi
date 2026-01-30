using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kutuphane.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GerekliUniqueKısımlarEklendiIliskilerAyarlandı : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OduncIslemler_Kitaplar_kitap_id",
                table: "OduncIslemler");

            migrationBuilder.DropForeignKey(
                name: "FK_OduncIslemler_Uyeler_uye_id",
                table: "OduncIslemler");

            migrationBuilder.CreateIndex(
                name: "IX_Uyeler_tc_no",
                table: "Uyeler",
                column: "tc_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kitaplar_isbn",
                table: "Kitaplar",
                column: "isbn",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OduncIslemler_Kitaplar_kitap_id",
                table: "OduncIslemler",
                column: "kitap_id",
                principalTable: "Kitaplar",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OduncIslemler_Uyeler_uye_id",
                table: "OduncIslemler",
                column: "uye_id",
                principalTable: "Uyeler",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OduncIslemler_Kitaplar_kitap_id",
                table: "OduncIslemler");

            migrationBuilder.DropForeignKey(
                name: "FK_OduncIslemler_Uyeler_uye_id",
                table: "OduncIslemler");

            migrationBuilder.DropIndex(
                name: "IX_Uyeler_tc_no",
                table: "Uyeler");

            migrationBuilder.DropIndex(
                name: "IX_Kitaplar_isbn",
                table: "Kitaplar");

            migrationBuilder.AddForeignKey(
                name: "FK_OduncIslemler_Kitaplar_kitap_id",
                table: "OduncIslemler",
                column: "kitap_id",
                principalTable: "Kitaplar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OduncIslemler_Uyeler_uye_id",
                table: "OduncIslemler",
                column: "uye_id",
                principalTable: "Uyeler",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
