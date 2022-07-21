using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class citas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentDomId",
                table: "cotizacionStudies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentLabId",
                table: "cotizacionStudies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CAT_Cita_Dom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpedienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NombrePaciente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estatus_Cita = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatsApp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Indicaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraCita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Cita_Dom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Cita_Dom_CAT_Expedientes_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "CAT_Expedientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Cita_Lab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpedienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NombrePaciente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procedencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompaniaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatsApp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SucursalID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraCita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Cita_Lab", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Cita_Lab_CAT_Expedientes_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "CAT_Expedientes",
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
                name: "IX_CAT_Cita_Dom_ExpedienteId",
                table: "CAT_Cita_Dom",
                column: "ExpedienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Cita_Lab_ExpedienteId",
                table: "CAT_Cita_Lab",
                column: "ExpedienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_cotizacionStudies_CAT_Cita_Dom_AppointmentDomId",
                table: "cotizacionStudies",
                column: "AppointmentDomId",
                principalTable: "CAT_Cita_Dom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cotizacionStudies_CAT_Cita_Lab_AppointmentLabId",
                table: "cotizacionStudies",
                column: "AppointmentLabId",
                principalTable: "CAT_Cita_Lab",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cotizacionStudies_CAT_Cita_Dom_AppointmentDomId",
                table: "cotizacionStudies");

            migrationBuilder.DropForeignKey(
                name: "FK_cotizacionStudies_CAT_Cita_Lab_AppointmentLabId",
                table: "cotizacionStudies");

            migrationBuilder.DropTable(
                name: "CAT_Cita_Dom");

            migrationBuilder.DropTable(
                name: "CAT_Cita_Lab");

            migrationBuilder.DropIndex(
                name: "IX_cotizacionStudies_AppointmentDomId",
                table: "cotizacionStudies");

            migrationBuilder.DropIndex(
                name: "IX_cotizacionStudies_AppointmentLabId",
                table: "cotizacionStudies");

            migrationBuilder.DropColumn(
                name: "AppointmentDomId",
                table: "cotizacionStudies");

            migrationBuilder.DropColumn(
                name: "AppointmentLabId",
                table: "cotizacionStudies");
        }
    }
}
