using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Billing.Migrations
{
    public partial class add_requests_invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Factura_Solicitudes_CAT_Factura_Companias_InvoiceCompanyId",
                table: "Relacion_Factura_Solicitudes");

            migrationBuilder.AlterColumn<Guid>(
                name: "InvoiceCompanyId",
                table: "Relacion_Factura_Solicitudes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceId",
                table: "Relacion_Factura_Solicitudes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Factura_Solicitudes_InvoiceId",
                table: "Relacion_Factura_Solicitudes",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Factura_Solicitudes_CAT_Factura_Companias_InvoiceCompanyId",
                table: "Relacion_Factura_Solicitudes",
                column: "InvoiceCompanyId",
                principalTable: "CAT_Factura_Companias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Factura_Solicitudes_CAT_Factura_InvoiceId",
                table: "Relacion_Factura_Solicitudes",
                column: "InvoiceId",
                principalTable: "CAT_Factura",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Factura_Solicitudes_CAT_Factura_Companias_InvoiceCompanyId",
                table: "Relacion_Factura_Solicitudes");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Factura_Solicitudes_CAT_Factura_InvoiceId",
                table: "Relacion_Factura_Solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Factura_Solicitudes_InvoiceId",
                table: "Relacion_Factura_Solicitudes");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Relacion_Factura_Solicitudes");

            migrationBuilder.AlterColumn<Guid>(
                name: "InvoiceCompanyId",
                table: "Relacion_Factura_Solicitudes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Factura_Solicitudes_CAT_Factura_Companias_InvoiceCompanyId",
                table: "Relacion_Factura_Solicitudes",
                column: "InvoiceCompanyId",
                principalTable: "CAT_Factura_Companias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
