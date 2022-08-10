using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class Relacion_Paquetes_EstudiosCascadeondelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_Relacion_Solicitud_Paquete_SolicitudId_PaqueteId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_Relacion_Solicitud_Paquete_SolicitudId_PaqueteId",
                table: "Relacion_Solicitud_Estudio",
                columns: new[] { "SolicitudId", "PaqueteId" },
                principalTable: "Relacion_Solicitud_Paquete",
                principalColumns: new[] { "SolicitudId", "PaqueteId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_Relacion_Solicitud_Paquete_SolicitudId_PaqueteId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_Relacion_Solicitud_Paquete_SolicitudId_PaqueteId",
                table: "Relacion_Solicitud_Estudio",
                columns: new[] { "SolicitudId", "PaqueteId" },
                principalTable: "Relacion_Solicitud_Paquete",
                principalColumns: new[] { "SolicitudId", "PaqueteId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
