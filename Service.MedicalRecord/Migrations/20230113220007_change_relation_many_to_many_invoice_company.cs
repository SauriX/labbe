using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class change_relation_many_to_many_invoice_company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "InvoiceCompanyRequest",
                columns: table => new
                {
                    FacturaCompañiaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolicitudesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceCompanyRequest", x => new { x.FacturaCompañiaId, x.SolicitudesId });
                    table.ForeignKey(
                        name: "FK_InvoiceCompanyRequest_CAT_Solicitud_SolicitudesId",
                        column: x => x.SolicitudesId,
                        principalTable: "CAT_Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceCompanyRequest_Relacion_Solicitud_Factura_Compania_FacturaCompañiaId",
                        column: x => x.FacturaCompañiaId,
                        principalTable: "Relacion_Solicitud_Factura_Compania",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceCompanyRequest_SolicitudesId",
                table: "InvoiceCompanyRequest",
                column: "SolicitudesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceCompanyRequest");

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
    }
}
