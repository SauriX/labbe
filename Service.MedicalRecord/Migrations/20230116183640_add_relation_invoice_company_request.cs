using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class add_relation_invoice_company_request : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceCompanyRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Relacion_Solicitud_Factura_Compania",
                table: "Relacion_Solicitud_Factura_Compania");

            migrationBuilder.DropColumn(
                name: "Estatus",
                table: "Relacion_Solicitud_Factura_Compania");

            migrationBuilder.DropColumn(
                name: "FacturapiId",
                table: "Relacion_Solicitud_Factura_Compania");

            migrationBuilder.RenameColumn(
                name: "FacturaId",
                table: "Relacion_Solicitud_Factura_Compania",
                newName: "InvoiceCompanyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Relacion_Solicitud_Factura_Compania",
                newName: "SolicitudId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreo",
                table: "Relacion_Solicitud_Factura_Compania",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Relacion_Solicitud_Factura_Compania",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relacion_Solicitud_Factura_Compania",
                table: "Relacion_Solicitud_Factura_Compania",
                columns: new[] { "SolicitudId", "InvoiceCompanyId" });

            migrationBuilder.CreateTable(
                name: "Factura_Compania",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoFactura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacturapiId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura_Compania", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Factura_Compania_InvoiceCompanyId",
                table: "Relacion_Solicitud_Factura_Compania",
                column: "InvoiceCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Factura_Compania_CAT_Solicitud_SolicitudId",
                table: "Relacion_Solicitud_Factura_Compania",
                column: "SolicitudId",
                principalTable: "CAT_Solicitud",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Factura_Compania_Factura_Compania_InvoiceCompanyId",
                table: "Relacion_Solicitud_Factura_Compania",
                column: "InvoiceCompanyId",
                principalTable: "Factura_Compania",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Factura_Compania_CAT_Solicitud_SolicitudId",
                table: "Relacion_Solicitud_Factura_Compania");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Factura_Compania_Factura_Compania_InvoiceCompanyId",
                table: "Relacion_Solicitud_Factura_Compania");

            migrationBuilder.DropTable(
                name: "Factura_Compania");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Relacion_Solicitud_Factura_Compania",
                table: "Relacion_Solicitud_Factura_Compania");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Solicitud_Factura_Compania_InvoiceCompanyId",
                table: "Relacion_Solicitud_Factura_Compania");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Relacion_Solicitud_Factura_Compania");

            migrationBuilder.RenameColumn(
                name: "InvoiceCompanyId",
                table: "Relacion_Solicitud_Factura_Compania",
                newName: "FacturaId");

            migrationBuilder.RenameColumn(
                name: "SolicitudId",
                table: "Relacion_Solicitud_Factura_Compania",
                newName: "Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreo",
                table: "Relacion_Solicitud_Factura_Compania",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<string>(
                name: "Estatus",
                table: "Relacion_Solicitud_Factura_Compania",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacturapiId",
                table: "Relacion_Solicitud_Factura_Compania",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relacion_Solicitud_Factura_Compania",
                table: "Relacion_Solicitud_Factura_Compania",
                column: "Id");

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
    }
}
