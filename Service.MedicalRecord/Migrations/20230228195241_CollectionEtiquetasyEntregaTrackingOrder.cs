using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class CollectionEtiquetasyEntregaTrackingOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TrackingOrderId",
                table: "Relacion_Solicitud_Etiquetas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEntrega",
                table: "CAT_Seguimiento_Ruta",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Etiquetas_TrackingOrderId",
                table: "Relacion_Solicitud_Etiquetas",
                column: "TrackingOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Etiquetas_CAT_Seguimiento_Ruta_TrackingOrderId",
                table: "Relacion_Solicitud_Etiquetas",
                column: "TrackingOrderId",
                principalTable: "CAT_Seguimiento_Ruta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Etiquetas_CAT_Seguimiento_Ruta_TrackingOrderId",
                table: "Relacion_Solicitud_Etiquetas");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Solicitud_Etiquetas_TrackingOrderId",
                table: "Relacion_Solicitud_Etiquetas");

            migrationBuilder.DropColumn(
                name: "TrackingOrderId",
                table: "Relacion_Solicitud_Etiquetas");

            migrationBuilder.DropColumn(
                name: "FechaEntrega",
                table: "CAT_Seguimiento_Ruta");
        }
    }
}
