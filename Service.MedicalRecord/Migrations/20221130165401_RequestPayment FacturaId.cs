using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class RequestPaymentFacturaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FacturaId",
                table: "Relacion_Solicitud_Pago",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FacturapiId",
                table: "Relacion_Solicitud_Pago",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacturaId",
                table: "Relacion_Solicitud_Pago");

            migrationBuilder.DropColumn(
                name: "FacturapiId",
                table: "Relacion_Solicitud_Pago");
        }
    }
}
