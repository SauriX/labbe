using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class RelacionListaprecioslealtades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_ListaPrecio_CAT_Lealtad_LealtadId",
                table: "CAT_ListaPrecio");

            migrationBuilder.DropIndex(
                name: "IX_CAT_ListaPrecio_LealtadId",
                table: "CAT_ListaPrecio");

            migrationBuilder.DropColumn(
                name: "LealtadId",
                table: "CAT_ListaPrecio");

            migrationBuilder.CreateTable(
                name: "Relacion_Loyalty_PrecioLista",
                columns: table => new
                {
                    PrecioListaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoyaltyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Loyalty_PrecioLista", x => new { x.LoyaltyId, x.PrecioListaId });
                    table.ForeignKey(
                        name: "FK_Relacion_Loyalty_PrecioLista_CAT_Lealtad_LoyaltyId",
                        column: x => x.LoyaltyId,
                        principalTable: "CAT_Lealtad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relacion_Loyalty_PrecioLista_CAT_ListaPrecio_PrecioListaId",
                        column: x => x.PrecioListaId,
                        principalTable: "CAT_ListaPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.AddColumn<Guid>(
                name: "LealtadId",
                table: "CAT_ListaPrecio",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaPrecio_LealtadId",
                table: "CAT_ListaPrecio",
                column: "LealtadId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_ListaPrecio_CAT_Lealtad_LealtadId",
                table: "CAT_ListaPrecio",
                column: "LealtadId",
                principalTable: "CAT_Lealtad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
