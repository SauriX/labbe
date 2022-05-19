using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class Correcciones_llaves_foraneas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_ListaP_Medicos_CAT_ListaPrecio_PriceListId",
                table: "CAT_ListaP_Medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_ListaP_Promocion_CAT_ListaPrecio_PriceListId",
                table: "CAT_ListaP_Promocion");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_ListaP_Sucursal_CAT_ListaPrecio_PriceListId",
                table: "CAT_ListaP_Sucursal");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_ListaP_Estudio_CAT_ListaPrecio_PriceListId",
                table: "Relacion_ListaP_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_ListaP_Paquete_CAT_ListaPrecio_PriceListId",
                table: "Relacion_ListaP_Paquete");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_ListaP_Paquete_PriceListId",
                table: "Relacion_ListaP_Paquete");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_ListaP_Estudio_PriceListId",
                table: "Relacion_ListaP_Estudio");

            migrationBuilder.DropIndex(
                name: "IX_CAT_ListaP_Sucursal_PriceListId",
                table: "CAT_ListaP_Sucursal");

            migrationBuilder.DropIndex(
                name: "IX_CAT_ListaP_Promocion_PriceListId",
                table: "CAT_ListaP_Promocion");

            migrationBuilder.DropIndex(
                name: "IX_CAT_ListaP_Medicos_PriceListId",
                table: "CAT_ListaP_Medicos");

            migrationBuilder.DropColumn(
                name: "PriceListId",
                table: "Relacion_ListaP_Paquete");

            migrationBuilder.DropColumn(
                name: "PriceListId",
                table: "Relacion_ListaP_Estudio");

            migrationBuilder.DropColumn(
                name: "PriceListId",
                table: "CAT_ListaP_Sucursal");

            migrationBuilder.DropColumn(
                name: "PriceListId",
                table: "CAT_ListaP_Promocion");

            migrationBuilder.DropColumn(
                name: "PriceListId",
                table: "CAT_ListaP_Medicos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PriceListId",
                table: "Relacion_ListaP_Paquete",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PriceListId",
                table: "Relacion_ListaP_Estudio",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PriceListId",
                table: "CAT_ListaP_Sucursal",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PriceListId",
                table: "CAT_ListaP_Promocion",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PriceListId",
                table: "CAT_ListaP_Medicos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_ListaP_Paquete_PriceListId",
                table: "Relacion_ListaP_Paquete",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_ListaP_Estudio_PriceListId",
                table: "Relacion_ListaP_Estudio",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaP_Sucursal_PriceListId",
                table: "CAT_ListaP_Sucursal",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaP_Promocion_PriceListId",
                table: "CAT_ListaP_Promocion",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaP_Medicos_PriceListId",
                table: "CAT_ListaP_Medicos",
                column: "PriceListId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_ListaP_Medicos_CAT_ListaPrecio_PriceListId",
                table: "CAT_ListaP_Medicos",
                column: "PriceListId",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_ListaP_Promocion_CAT_ListaPrecio_PriceListId",
                table: "CAT_ListaP_Promocion",
                column: "PriceListId",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_ListaP_Sucursal_CAT_ListaPrecio_PriceListId",
                table: "CAT_ListaP_Sucursal",
                column: "PriceListId",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_ListaP_Estudio_CAT_ListaPrecio_PriceListId",
                table: "Relacion_ListaP_Estudio",
                column: "PriceListId",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_ListaP_Paquete_CAT_ListaPrecio_PriceListId",
                table: "Relacion_ListaP_Paquete",
                column: "PriceListId",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
