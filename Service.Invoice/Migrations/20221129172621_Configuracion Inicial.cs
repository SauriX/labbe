using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Billing.Migrations
{
    public partial class ConfiguracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Factura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacturapiId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormaPago = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetodoPago = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsoCFDI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegimenFiscal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desglozado = table.Column<bool>(type: "bit", nullable: false),
                    ConNombre = table.Column<bool>(type: "bit", nullable: false),
                    EnvioCorreo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnvioWhatsapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Solicitud = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpedienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Expediente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Paciente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Factura", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Factura");
        }
    }
}
