using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class EliminarConfigLealtadListaPrecios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Loyalty_PrecioLista_CAT_ListaPrecio_PrecioListaId",
                table: "Relacion_Loyalty_PrecioLista");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Loyalty_PrecioLista_CAT_ListaPrecio_PrecioListaId",
                table: "Relacion_Loyalty_PrecioLista",
                column: "PrecioListaId",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Loyalty_PrecioLista_CAT_ListaPrecio_PrecioListaId",
                table: "Relacion_Loyalty_PrecioLista");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Loyalty_PrecioLista_CAT_ListaPrecio_PrecioListaId",
                table: "Relacion_Loyalty_PrecioLista",
                column: "PrecioListaId",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
