using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Billing.Migrations
{
    public partial class delete_invoice_compamy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Factura_Solicitudes");

            migrationBuilder.DropTable(
                name: "CAT_Factura_Companias");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Factura_Companias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Compañia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompañiaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConNombre = table.Column<bool>(type: "bit", nullable: false),
                    Desglozado = table.Column<bool>(type: "bit", nullable: false),
                    EnvioCorreo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnvioWhatsapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacturapiId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FormaPago = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetodoPago = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegimenFiscal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsoCFDI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Factura_Companias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Factura_Solicitudes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvoiceCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Factura_Solicitudes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacion_Factura_Solicitudes_CAT_Factura_Companias_InvoiceCompanyId",
                        column: x => x.InvoiceCompanyId,
                        principalTable: "CAT_Factura_Companias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Factura_Solicitudes_InvoiceCompanyId",
                table: "Relacion_Factura_Solicitudes",
                column: "InvoiceCompanyId");
        }
    }
}
