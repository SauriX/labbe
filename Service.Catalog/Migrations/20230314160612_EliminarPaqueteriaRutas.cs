using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class EliminarPaqueteriaRutas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Rutas_CAT_Paqueteria_PaqueteriaId",
                table: "CAT_Rutas");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Rutas_CAT_Sucursal_SucursalDestinoId",
                table: "CAT_Rutas");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Rutas_CAT_Sucursal_SucursalOrigenId",
                table: "CAT_Rutas");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Rutas_PaqueteriaId",
                table: "CAT_Rutas");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Rutas_SucursalDestinoId",
                table: "CAT_Rutas");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Rutas_SucursalOrigenId",
                table: "CAT_Rutas");

            migrationBuilder.DropColumn(
                name: "PaqueteriaId",
                table: "CAT_Rutas");

            migrationBuilder.DropColumn(
                name: "SucursalDestinoId",
                table: "CAT_Rutas");

            migrationBuilder.DropColumn(
                name: "SucursalOrigenId",
                table: "CAT_Rutas");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Rutas_DestinoId",
                table: "CAT_Rutas",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Rutas_OrigenId",
                table: "CAT_Rutas",
                column: "OrigenId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Rutas_CAT_Sucursal_DestinoId",
                table: "CAT_Rutas",
                column: "DestinoId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Rutas_CAT_Sucursal_OrigenId",
                table: "CAT_Rutas",
                column: "OrigenId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Rutas_CAT_Sucursal_DestinoId",
                table: "CAT_Rutas");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Rutas_CAT_Sucursal_OrigenId",
                table: "CAT_Rutas");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Rutas_DestinoId",
                table: "CAT_Rutas");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Rutas_OrigenId",
                table: "CAT_Rutas");

            migrationBuilder.AddColumn<int>(
                name: "PaqueteriaId",
                table: "CAT_Rutas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SucursalDestinoId",
                table: "CAT_Rutas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SucursalOrigenId",
                table: "CAT_Rutas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Rutas_PaqueteriaId",
                table: "CAT_Rutas",
                column: "PaqueteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Rutas_SucursalDestinoId",
                table: "CAT_Rutas",
                column: "SucursalDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Rutas_SucursalOrigenId",
                table: "CAT_Rutas",
                column: "SucursalOrigenId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Rutas_CAT_Paqueteria_PaqueteriaId",
                table: "CAT_Rutas",
                column: "PaqueteriaId",
                principalTable: "CAT_Paqueteria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Rutas_CAT_Sucursal_SucursalDestinoId",
                table: "CAT_Rutas",
                column: "SucursalDestinoId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Rutas_CAT_Sucursal_SucursalOrigenId",
                table: "CAT_Rutas",
                column: "SucursalOrigenId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
