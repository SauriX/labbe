using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class relacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SolicitudId",
                table: "cotizacionStudies",
                newName: "CotizacionId");

            migrationBuilder.AddColumn<Guid>(
                name: "PriceQuoteId",
                table: "cotizacionStudies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_cotizacionStudies",
                table: "cotizacionStudies",
                column: "CotizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_cotizacionStudies_PriceQuoteId",
                table: "cotizacionStudies",
                column: "PriceQuoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_cotizacionStudies_CAT_Cotizaciones_PriceQuoteId",
                table: "cotizacionStudies",
                column: "PriceQuoteId",
                principalTable: "CAT_Cotizaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cotizacionStudies_CAT_Cotizaciones_PriceQuoteId",
                table: "cotizacionStudies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cotizacionStudies",
                table: "cotizacionStudies");

            migrationBuilder.DropIndex(
                name: "IX_cotizacionStudies_PriceQuoteId",
                table: "cotizacionStudies");

            migrationBuilder.DropColumn(
                name: "PriceQuoteId",
                table: "cotizacionStudies");

            migrationBuilder.RenameColumn(
                name: "CotizacionId",
                table: "cotizacionStudies",
                newName: "SolicitudId");
        }
    }
}
