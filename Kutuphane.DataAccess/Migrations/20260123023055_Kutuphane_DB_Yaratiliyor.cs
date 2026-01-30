using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kutuphane.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Kutuphane_DB_Yaratiliyor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kitaplar",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    isbn = table.Column<int>(type: "integer", nullable: false),
                    baslik = table.Column<string>(type: "text", nullable: false),
                    yazar = table.Column<string>(type: "text", nullable: false),
                    yayin_evi = table.Column<string>(type: "text", nullable: false),
                    kategori = table.Column<string>(type: "text", nullable: false),
                    sayfa_sayisi = table.Column<int>(type: "integer", nullable: false),
                    yayin_yili = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitaplar", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Uyeler",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tc_no = table.Column<int>(type: "integer", nullable: false),
                    ad = table.Column<string>(type: "text", nullable: false),
                    soyad = table.Column<string>(type: "text", nullable: false),
                    telefon_numarasi = table.Column<int>(type: "integer", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    adres = table.Column<string>(type: "text", nullable: false),
                    kayit_tarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uyeler", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kitaplar");

            migrationBuilder.DropTable(
                name: "Uyeler");
        }
    }
}
