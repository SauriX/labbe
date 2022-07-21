using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Service.MedicalRecord.Migrations
{
    public partial class estudioscotizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cotizacionStudies",
                columns: table => new
                {
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListaPrecioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromocionId = table.Column<int>(type: "int", nullable: false),
                    EstudioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaqueteId = table.Column<int>(type: "int", nullable: true),
                    EstatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    Descuento = table.Column<bool>(type: "bit", nullable: false),
                    Cargo = table.Column<bool>(type: "bit", nullable: false),
                    Copago = table.Column<bool>(type: "bit", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cotizacionStudies");
        }
    }
}
