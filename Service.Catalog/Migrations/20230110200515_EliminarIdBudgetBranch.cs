using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class EliminarIdBudgetBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Presupuesto_Sucursal_CAT_Presupuestos_CostoFijoId",
                table: "Relacion_Presupuesto_Sucursal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Relacion_Presupuesto_Sucursal",
                table: "Relacion_Presupuesto_Sucursal");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Presupuesto_Sucursal_SucursalId",
                table: "Relacion_Presupuesto_Sucursal");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Relacion_Presupuesto_Sucursal");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioModificoId",
                table: "Relacion_Presupuesto_Sucursal",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModifico",
                table: "Relacion_Presupuesto_Sucursal",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relacion_Presupuesto_Sucursal",
                table: "Relacion_Presupuesto_Sucursal",
                columns: new[] { "SucursalId", "CostoFijoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Presupuesto_Sucursal_CAT_Presupuestos_CostoFijoId",
                table: "Relacion_Presupuesto_Sucursal",
                column: "CostoFijoId",
                principalTable: "CAT_Presupuestos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Presupuesto_Sucursal_CAT_Presupuestos_CostoFijoId",
                table: "Relacion_Presupuesto_Sucursal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Relacion_Presupuesto_Sucursal",
                table: "Relacion_Presupuesto_Sucursal");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioModificoId",
                table: "Relacion_Presupuesto_Sucursal",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModifico",
                table: "Relacion_Presupuesto_Sucursal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Relacion_Presupuesto_Sucursal",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relacion_Presupuesto_Sucursal",
                table: "Relacion_Presupuesto_Sucursal",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Presupuesto_Sucursal_SucursalId",
                table: "Relacion_Presupuesto_Sucursal",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Presupuesto_Sucursal_CAT_Presupuestos_CostoFijoId",
                table: "Relacion_Presupuesto_Sucursal",
                column: "CostoFijoId",
                principalTable: "CAT_Presupuestos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
