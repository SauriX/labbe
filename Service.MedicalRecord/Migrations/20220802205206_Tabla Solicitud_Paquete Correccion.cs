using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class TablaSolicitud_PaqueteCorreccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Paquete_CAT_Solicitud_RequestId",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Solicitud_Paquete_RequestId",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Paquete_CAT_Solicitud_SolicitudId",
                table: "Relacion_Solicitud_Paquete",
                column: "SolicitudId",
                principalTable: "CAT_Solicitud",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Paquete_CAT_Solicitud_SolicitudId",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.AddColumn<Guid>(
                name: "RequestId",
                table: "Relacion_Solicitud_Paquete",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Paquete_RequestId",
                table: "Relacion_Solicitud_Paquete",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Paquete_CAT_Solicitud_RequestId",
                table: "Relacion_Solicitud_Paquete",
                column: "RequestId",
                principalTable: "CAT_Solicitud",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
