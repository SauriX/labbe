using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Service.MedicalRecord.Migrations
{
    public partial class addestudy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Expedientes_ExpedienteId",
                table: "CAT_Cotizaciones");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExpedienteId",
                table: "CAT_Cotizaciones",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Expedientes_ExpedienteId",
                table: "CAT_Cotizaciones",
                column: "ExpedienteId",
                principalTable: "CAT_Expedientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Expedientes_ExpedienteId",
                table: "CAT_Cotizaciones");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExpedienteId",
                table: "CAT_Cotizaciones",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Expedientes_ExpedienteId",
                table: "CAT_Cotizaciones",
                column: "ExpedienteId",
                principalTable: "CAT_Expedientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
