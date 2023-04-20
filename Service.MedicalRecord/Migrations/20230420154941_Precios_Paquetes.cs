using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class Precios_Paquetes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEntrega",
                table: "Relacion_Solicitud_Paquete",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "PaqueteDescuento",
                table: "Relacion_Solicitud_Paquete",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PaqueteDescuentoProcentaje",
                table: "Relacion_Solicitud_Paquete",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioEstudios",
                table: "Relacion_Solicitud_Paquete",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaEntrega",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropColumn(
                name: "PaqueteDescuento",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropColumn(
                name: "PaqueteDescuentoProcentaje",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropColumn(
                name: "PrecioEstudios",
                table: "Relacion_Solicitud_Paquete");
        }
    }
}
