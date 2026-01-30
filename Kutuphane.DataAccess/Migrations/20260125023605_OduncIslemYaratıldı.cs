using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kutuphane.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class OduncIslemYaratıldı : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OduncIslem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KitapId = table.Column<int>(type: "integer", nullable: false),
                    UyeId = table.Column<int>(type: "integer", nullable: false),
                    AlmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SonIadeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IadeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IadeEdildiMi = table.Column<bool>(type: "boolean", nullable: false),
                    GecikmeGunSayisi = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OduncIslem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OduncIslem_Kitaplar_KitapId",
                        column: x => x.KitapId,
                        principalTable: "Kitaplar",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OduncIslem_Uyeler_UyeId",
                        column: x => x.UyeId,
                        principalTable: "Uyeler",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OduncIslem_KitapId",
                table: "OduncIslem",
                column: "KitapId");

            migrationBuilder.CreateIndex(
                name: "IX_OduncIslem_UyeId",
                table: "OduncIslem",
                column: "UyeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OduncIslem");
        }
    }
}
