using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class AregarLPPromoEnCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrecioListaId",
                table: "CAT_Compañia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PromocionesId",
                table: "CAT_Compañia",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Compañia_PrecioListaId",
                table: "CAT_Compañia",
                column: "PrecioListaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Compañia_PromocionesId",
                table: "CAT_Compañia",
                column: "PromocionesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Compañia_CAT_ListaPrecio_PrecioListaId",
                table: "CAT_Compañia",
                column: "PrecioListaId",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Compañia_CAT_Promocion_PromocionesId",
                table: "CAT_Compañia",
                column: "PromocionesId",
                principalTable: "CAT_Promocion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Compañia_CAT_ListaPrecio_PrecioListaId",
                table: "CAT_Compañia");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Compañia_CAT_Promocion_PromocionesId",
                table: "CAT_Compañia");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Compañia_PrecioListaId",
                table: "CAT_Compañia");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Compañia_PromocionesId",
                table: "CAT_Compañia");

            migrationBuilder.DropColumn(
                name: "PrecioListaId",
                table: "CAT_Compañia");

            migrationBuilder.DropColumn(
                name: "PromocionesId",
                table: "CAT_Compañia");
        }
    }
}
