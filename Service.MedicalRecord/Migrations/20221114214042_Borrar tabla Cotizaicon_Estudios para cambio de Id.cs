using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class BorrartablaCotizaicon_EstudiosparacambiodeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cotizacionStudies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cotizacionStudies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AplicaCargo = table.Column<bool>(type: "bit", nullable: false),
                    AppointmentDomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AppointmentLabId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CotizacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dias = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Horas = table.Column<int>(type: "int", nullable: false),
                    ListaPrecio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListaPrecioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaqueteId = table.Column<int>(type: "int", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cotizacionStudies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cotizacionStudies_CAT_Cita_Dom_AppointmentDomId",
                        column: x => x.AppointmentDomId,
                        principalTable: "CAT_Cita_Dom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cotizacionStudies_CAT_Cita_Lab_AppointmentLabId",
                        column: x => x.AppointmentLabId,
                        principalTable: "CAT_Cita_Lab",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cotizacionStudies_CAT_Cotizaciones_CotizacionId",
                        column: x => x.CotizacionId,
                        principalTable: "CAT_Cotizaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cotizacionStudies_Relacion_Solicitud_Paquete_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Relacion_Solicitud_Paquete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cotizacionStudies_AppointmentDomId",
                table: "cotizacionStudies",
                column: "AppointmentDomId");

            migrationBuilder.CreateIndex(
                name: "IX_cotizacionStudies_AppointmentLabId",
                table: "cotizacionStudies",
                column: "AppointmentLabId");

            migrationBuilder.CreateIndex(
                name: "IX_cotizacionStudies_CotizacionId",
                table: "cotizacionStudies",
                column: "CotizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_cotizacionStudies_PaqueteId",
                table: "cotizacionStudies",
                column: "PaqueteId");
        }
    }
}
