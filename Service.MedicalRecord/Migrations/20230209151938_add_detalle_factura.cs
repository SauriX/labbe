using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class add_detalle_factura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CantidadTotal",
                table: "Factura_Compania",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "CompañiaId",
                table: "Factura_Compania",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExpedienteId",
                table: "Factura_Compania",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FormaPago",
                table: "Factura_Compania",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FormaPagoId",
                table: "Factura_Compania",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "IVA",
                table: "Factura_Compania",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "NumeroCuenta",
                table: "Factura_Compania",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Serie",
                table: "Factura_Compania",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "Factura_Compania",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "TaxDataId",
                table: "Factura_Compania",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoDesgloce",
                table: "Factura_Compania",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsoCFDI",
                table: "Factura_Compania",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Relacion_Factura_Detalle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolicitudClave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstudioClave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Concepto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Importe = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Factura_Detalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacion_Factura_Detalle_Factura_Compania_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Factura_Compania",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "Relacion_Solicitud_Etiquetas",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Suero = table.Column<int>(type: "int", nullable: false),
            //        Tapon = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Orden = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Relacion_Solicitud_Etiquetas", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Relacion_Solicitud_Etiquetas_CAT_Solicitud_SolicitudId",
            //            column: x => x.SolicitudId,
            //            principalTable: "CAT_Solicitud",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_Factura_Compania_CompañiaId",
                table: "Factura_Compania",
                column: "CompañiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_Compania_TaxDataId",
                table: "Factura_Compania",
                column: "TaxDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Factura_Detalle_FacturaId",
                table: "Relacion_Factura_Detalle",
                column: "FacturaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Relacion_Solicitud_Etiquetas_SolicitudId",
            //    table: "Relacion_Solicitud_Etiquetas",
            //    column: "SolicitudId");

            migrationBuilder.AddForeignKey(
                name: "FK_Factura_Compania_CAT_Compañia_CompañiaId",
                table: "Factura_Compania",
                column: "CompañiaId",
                principalTable: "CAT_Compañia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Factura_Compania_CAT_Datos_Fiscales_TaxDataId",
                table: "Factura_Compania",
                column: "TaxDataId",
                principalTable: "CAT_Datos_Fiscales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factura_Compania_CAT_Compañia_CompañiaId",
                table: "Factura_Compania");

            migrationBuilder.DropForeignKey(
                name: "FK_Factura_Compania_CAT_Datos_Fiscales_TaxDataId",
                table: "Factura_Compania");

            migrationBuilder.DropTable(
                name: "Relacion_Factura_Detalle");

            //migrationBuilder.DropTable(
            //    name: "Relacion_Solicitud_Etiquetas");

            migrationBuilder.DropIndex(
                name: "IX_Factura_Compania_CompañiaId",
                table: "Factura_Compania");

            migrationBuilder.DropIndex(
                name: "IX_Factura_Compania_TaxDataId",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "CantidadTotal",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "CompañiaId",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "ExpedienteId",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "FormaPago",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "FormaPagoId",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "IVA",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "NumeroCuenta",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "Serie",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "TaxDataId",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "TipoDesgloce",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "UsoCFDI",
                table: "Factura_Compania");
        }
    }
}
