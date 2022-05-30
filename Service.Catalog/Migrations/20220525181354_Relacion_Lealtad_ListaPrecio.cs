using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class Relacion_Lealtad_ListaPrecio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relacion_Loyalty_PrecioLista",
                columns: table => new
                {
                    PrecioListaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoyaltyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Loyalty_PrecioLista", x => new { x.LoyaltyId, x.PrecioListaId });
                    table.ForeignKey(
                        name: "FK_Relacion_Loyalty_PrecioLista_CAT_Lealtad_LoyaltyId",
                        column: x => x.LoyaltyId,
                        principalTable: "CAT_Lealtad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Loyalty_PrecioLista_CAT_ListaPrecio_PrecioListaId",
                        column: x => x.PrecioListaId,
                        principalTable: "CAT_ListaPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Loyalty_PrecioLista_PrecioListaId",
                table: "Relacion_Loyalty_PrecioLista",
                column: "PrecioListaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Loyalty_PrecioLista");
        }
    }
}
