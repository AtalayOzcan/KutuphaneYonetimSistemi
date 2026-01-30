using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kutuphane.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class OduncIslemlerEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OduncIslem_Kitaplar_KitapId",
                table: "OduncIslem");

            migrationBuilder.DropForeignKey(
                name: "FK_OduncIslem_Uyeler_UyeId",
                table: "OduncIslem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OduncIslem",
                table: "OduncIslem");

            migrationBuilder.RenameTable(
                name: "OduncIslem",
                newName: "OduncIslemler");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OduncIslemler",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UyeId",
                table: "OduncIslemler",
                newName: "uye_id");

            migrationBuilder.RenameColumn(
                name: "SonIadeTarihi",
                table: "OduncIslemler",
                newName: "son_iade_tarihi");

            migrationBuilder.RenameColumn(
                name: "KitapId",
                table: "OduncIslemler",
                newName: "kitap_id");

            migrationBuilder.RenameColumn(
                name: "IadeTarihi",
                table: "OduncIslemler",
                newName: "iade_tarihi");

            migrationBuilder.RenameColumn(
                name: "IadeEdildiMi",
                table: "OduncIslemler",
                newName: "iade_edildi_mi");

            migrationBuilder.RenameColumn(
                name: "GecikmeGunSayisi",
                table: "OduncIslemler",
                newName: "gecikme_gun_sayisi");

            migrationBuilder.RenameColumn(
                name: "AlmaTarihi",
                table: "OduncIslemler",
                newName: "alma_tarihi");

            migrationBuilder.RenameIndex(
                name: "IX_OduncIslem_UyeId",
                table: "OduncIslemler",
                newName: "IX_OduncIslemler_uye_id");

            migrationBuilder.RenameIndex(
                name: "IX_OduncIslem_KitapId",
                table: "OduncIslemler",
                newName: "IX_OduncIslemler_kitap_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "iade_tarihi",
                table: "OduncIslemler",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OduncIslemler",
                table: "OduncIslemler",
                column: "id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OduncIslemler_Kitaplar_kitap_id",
                table: "OduncIslemler");

            migrationBuilder.DropForeignKey(
                name: "FK_OduncIslemler_Uyeler_uye_id",
                table: "OduncIslemler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OduncIslemler",
                table: "OduncIslemler");

            migrationBuilder.RenameTable(
                name: "OduncIslemler",
                newName: "OduncIslem");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OduncIslem",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "uye_id",
                table: "OduncIslem",
                newName: "UyeId");

            migrationBuilder.RenameColumn(
                name: "son_iade_tarihi",
                table: "OduncIslem",
                newName: "SonIadeTarihi");

            migrationBuilder.RenameColumn(
                name: "kitap_id",
                table: "OduncIslem",
                newName: "KitapId");

            migrationBuilder.RenameColumn(
                name: "iade_tarihi",
                table: "OduncIslem",
                newName: "IadeTarihi");

            migrationBuilder.RenameColumn(
                name: "iade_edildi_mi",
                table: "OduncIslem",
                newName: "IadeEdildiMi");

            migrationBuilder.RenameColumn(
                name: "gecikme_gun_sayisi",
                table: "OduncIslem",
                newName: "GecikmeGunSayisi");

            migrationBuilder.RenameColumn(
                name: "alma_tarihi",
                table: "OduncIslem",
                newName: "AlmaTarihi");

            migrationBuilder.RenameIndex(
                name: "IX_OduncIslemler_uye_id",
                table: "OduncIslem",
                newName: "IX_OduncIslem_UyeId");

            migrationBuilder.RenameIndex(
                name: "IX_OduncIslemler_kitap_id",
                table: "OduncIslem",
                newName: "IX_OduncIslem_KitapId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IadeTarihi",
                table: "OduncIslem",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OduncIslem",
                table: "OduncIslem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OduncIslem_Kitaplar_KitapId",
                table: "OduncIslem",
                column: "KitapId",
                principalTable: "Kitaplar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OduncIslem_Uyeler_UyeId",
                table: "OduncIslem",
                column: "UyeId",
                principalTable: "Uyeler",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
