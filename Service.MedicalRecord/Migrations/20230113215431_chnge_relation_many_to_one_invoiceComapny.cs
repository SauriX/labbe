using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class chnge_relation_many_to_one_invoiceComapny : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Factura_Compania_CAT_Solicitud_SolicitudId",
                table: "Relacion_Solicitud_Factura_Compania");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Solicitud_Factura_Compania_SolicitudId",
                table: "Relacion_Solicitud_Factura_Compania");

            migrationBuilder.DropColumn(
                name: "SolicitudId",
                table: "Relacion_Solicitud_Factura_Compania");

            migrationBuilder.AddColumn<Guid>(
                name: "FacturaCompañiaId",
                table: "CAT_Solicitud",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Solicitud_FacturaCompañiaId",
                table: "CAT_Solicitud",
                column: "FacturaCompañiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Solicitud_Relacion_Solicitud_Factura_Compania_FacturaCompañiaId",
                table: "CAT_Solicitud",
                column: "FacturaCompañiaId",
                principalTable: "Relacion_Solicitud_Factura_Compania",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Solicitud_Relacion_Solicitud_Factura_Compania_FacturaCompañiaId",
                table: "CAT_Solicitud");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Solicitud_FacturaCompañiaId",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "FacturaCompañiaId",
                table: "CAT_Solicitud");

            migrationBuilder.AddColumn<Guid>(
                name: "SolicitudId",
                table: "Relacion_Solicitud_Factura_Compania",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Factura_Compania_SolicitudId",
                table: "Relacion_Solicitud_Factura_Compania",
                column: "SolicitudId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Factura_Compania_CAT_Solicitud_SolicitudId",
                table: "Relacion_Solicitud_Factura_Compania",
                column: "SolicitudId",
                principalTable: "CAT_Solicitud",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
