using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class exedientecotizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExpedienteId",
                table: "CAT_Cotizaciones",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Cotizaciones_ExpedienteId",
                table: "CAT_Cotizaciones",
                column: "ExpedienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Expedientes_ExpedienteId",
                table: "CAT_Cotizaciones",
                column: "ExpedienteId",
                principalTable: "CAT_Expedientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Expedientes_ExpedienteId",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Cotizaciones_ExpedienteId",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropColumn(
                name: "ExpedienteId",
                table: "CAT_Cotizaciones");
        }
    }
}
