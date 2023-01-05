using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Report.Migrations
{
    public partial class IndicadoresReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Indicadores_CAT_Sucursal_SucursalId",
                table: "CAT_Indicadores");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Indicadores_SucursalId",
                table: "CAT_Indicadores");

            migrationBuilder.DropColumn(
                name: "CostoFijo",
                table: "CAT_Indicadores");

            migrationBuilder.DropColumn(
                name: "CostoTomaCalculado",
                table: "CAT_Indicadores");

            migrationBuilder.DropColumn(
                name: "FechaFinal",
                table: "CAT_Indicadores");

            migrationBuilder.DropColumn(
                name: "Ingresos",
                table: "CAT_Indicadores");

            migrationBuilder.DropColumn(
                name: "Pacientes",
                table: "CAT_Indicadores");

            migrationBuilder.DropColumn(
                name: "UtilidadOperacion",
                table: "CAT_Indicadores");

            migrationBuilder.RenameColumn(
                name: "SolicitudId",
                table: "CAT_Solicitud",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FechaInicial",
                table: "CAT_Indicadores",
                newName: "Fecha");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CAT_Solicitud",
                newName: "SolicitudId");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "CAT_Indicadores",
                newName: "FechaInicial");

            migrationBuilder.AddColumn<decimal>(
                name: "CostoFijo",
                table: "CAT_Indicadores",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CostoTomaCalculado",
                table: "CAT_Indicadores",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFinal",
                table: "CAT_Indicadores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Ingresos",
                table: "CAT_Indicadores",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Pacientes",
                table: "CAT_Indicadores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UtilidadOperacion",
                table: "CAT_Indicadores",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Indicadores_SucursalId",
                table: "CAT_Indicadores",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Indicadores_CAT_Sucursal_SucursalId",
                table: "CAT_Indicadores",
                column: "SucursalId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
