using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class requeststudyintrakingorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SolicitudEstudioId",
                table: "CAT_Seguimiento_Ruta",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "SolicitudEstudioId1",
                table: "CAT_Seguimiento_Ruta",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Seguimiento_Ruta_SolicitudEstudioId1",
                table: "CAT_Seguimiento_Ruta",
                column: "SolicitudEstudioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Seguimiento_Ruta_Relacion_Solicitud_Estudio_SolicitudEstudioId1",
                table: "CAT_Seguimiento_Ruta",
                column: "SolicitudEstudioId1",
                principalTable: "Relacion_Solicitud_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Seguimiento_Ruta_Relacion_Solicitud_Estudio_SolicitudEstudioId1",
                table: "CAT_Seguimiento_Ruta");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Seguimiento_Ruta_SolicitudEstudioId1",
                table: "CAT_Seguimiento_Ruta");

            migrationBuilder.DropColumn(
                name: "SolicitudEstudioId",
                table: "CAT_Seguimiento_Ruta");

            migrationBuilder.DropColumn(
                name: "SolicitudEstudioId1",
                table: "CAT_Seguimiento_Ruta");
        }
    }
}
