using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class AgregarIdRelacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Relacion_Presupuesto_Sucursal",
                table: "Relacion_Presupuesto_Sucursal");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Relacion_Presupuesto_Sucursal",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relacion_Presupuesto_Sucursal",
                table: "Relacion_Presupuesto_Sucursal",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Presupuesto_Sucursal_SucursalId",
                table: "Relacion_Presupuesto_Sucursal",
                column: "SucursalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Relacion_Presupuesto_Sucursal",
                table: "Relacion_Presupuesto_Sucursal");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Presupuesto_Sucursal_SucursalId",
                table: "Relacion_Presupuesto_Sucursal");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Relacion_Presupuesto_Sucursal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relacion_Presupuesto_Sucursal",
                table: "Relacion_Presupuesto_Sucursal",
                columns: new[] { "SucursalId", "CostoFijoId" });
        }
    }
}
