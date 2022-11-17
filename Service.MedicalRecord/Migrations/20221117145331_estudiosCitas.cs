using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class estudiosCitas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentDomId",
                table: "Relacion_Cotizacion_Estudio",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentLabId",
                table: "Relacion_Cotizacion_Estudio",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Cotizacion_Estudio_AppointmentDomId",
                table: "Relacion_Cotizacion_Estudio",
                column: "AppointmentDomId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Cotizacion_Estudio_AppointmentLabId",
                table: "Relacion_Cotizacion_Estudio",
                column: "AppointmentLabId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Cotizacion_Estudio_CAT_Cita_Dom_AppointmentDomId",
                table: "Relacion_Cotizacion_Estudio",
                column: "AppointmentDomId",
                principalTable: "CAT_Cita_Dom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Cotizacion_Estudio_CAT_Cita_Lab_AppointmentLabId",
                table: "Relacion_Cotizacion_Estudio",
                column: "AppointmentLabId",
                principalTable: "CAT_Cita_Lab",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Cotizacion_Estudio_CAT_Cita_Dom_AppointmentDomId",
                table: "Relacion_Cotizacion_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Cotizacion_Estudio_CAT_Cita_Lab_AppointmentLabId",
                table: "Relacion_Cotizacion_Estudio");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Cotizacion_Estudio_AppointmentDomId",
                table: "Relacion_Cotizacion_Estudio");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Cotizacion_Estudio_AppointmentLabId",
                table: "Relacion_Cotizacion_Estudio");

            migrationBuilder.DropColumn(
                name: "AppointmentDomId",
                table: "Relacion_Cotizacion_Estudio");

            migrationBuilder.DropColumn(
                name: "AppointmentLabId",
                table: "Relacion_Cotizacion_Estudio");
        }
    }
}
