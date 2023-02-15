using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class add_total_invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormaPagoId",
                table: "Factura_Compania");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Factura_Compania",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Factura_Compania");

            migrationBuilder.AddColumn<Guid>(
                name: "FormaPagoId",
                table: "Factura_Compania",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
