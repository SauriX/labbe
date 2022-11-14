using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class Cambiosencotizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cotizacionStudies_CAT_Cotizaciones_PriceQuoteId",
                table: "cotizacionStudies");

            migrationBuilder.DropIndex(
                name: "IX_cotizacionStudies_PriceQuoteId",
                table: "cotizacionStudies");

            migrationBuilder.DropColumn(
                name: "Cargo",
                table: "cotizacionStudies");

            migrationBuilder.DropColumn(
                name: "Copago",
                table: "cotizacionStudies");

            migrationBuilder.DropColumn(
                name: "EstatusId",
                table: "cotizacionStudies");

            migrationBuilder.DropColumn(
                name: "PriceQuoteId",
                table: "cotizacionStudies");

            migrationBuilder.DropColumn(
                name: "PromocionId",
                table: "cotizacionStudies");

            migrationBuilder.DropColumn(
                name: "CompaniaId",
                table: "CAT_Cotizaciones");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "cotizacionStudies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Descuento",
                table: "cotizacionStudies",
                newName: "AplicaCargo");

            migrationBuilder.RenameColumn(
                name: "Whatsapp",
                table: "CAT_Cotizaciones",
                newName: "EnvioWhatsapp");

            migrationBuilder.RenameColumn(
                name: "FechaNac",
                table: "CAT_Cotizaciones",
                newName: "FechaNacimiento");

            migrationBuilder.RenameColumn(
                name: "Correo",
                table: "CAT_Cotizaciones",
                newName: "EnvioCorreo");

            migrationBuilder.RenameColumn(
                name: "Afiliacion",
                table: "CAT_Cotizaciones",
                newName: "Clave");

            migrationBuilder.AlterColumn<int>(
                name: "EstudioId",
                table: "cotizacionStudies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Dias",
                table: "cotizacionStudies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Horas",
                table: "cotizacionStudies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ListaPrecio",
                table: "cotizacionStudies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "cotizacionStudies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MedicoId",
                table: "CAT_Cotizaciones",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CompañiaId",
                table: "CAT_Cotizaciones",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SucursalId",
                table: "CAT_Cotizaciones",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cotizacionStudies_CotizacionId",
                table: "cotizacionStudies",
                column: "CotizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_cotizacionStudies_PaqueteId",
                table: "cotizacionStudies",
                column: "PaqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Cotizaciones_CompañiaId",
                table: "CAT_Cotizaciones",
                column: "CompañiaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Cotizaciones_MedicoId",
                table: "CAT_Cotizaciones",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Cotizaciones_SucursalId",
                table: "CAT_Cotizaciones",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Compañia_CompañiaId",
                table: "CAT_Cotizaciones",
                column: "CompañiaId",
                principalTable: "CAT_Compañia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Medico_MedicoId",
                table: "CAT_Cotizaciones",
                column: "MedicoId",
                principalTable: "CAT_Medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Sucursal_SucursalId",
                table: "CAT_Cotizaciones",
                column: "SucursalId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cotizacionStudies_CAT_Cotizaciones_CotizacionId",
                table: "cotizacionStudies",
                column: "CotizacionId",
                principalTable: "CAT_Cotizaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cotizacionStudies_Relacion_Solicitud_Paquete_PaqueteId",
                table: "cotizacionStudies",
                column: "PaqueteId",
                principalTable: "Relacion_Solicitud_Paquete",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Compañia_CompañiaId",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Medico_MedicoId",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Sucursal_SucursalId",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_cotizacionStudies_CAT_Cotizaciones_CotizacionId",
                table: "cotizacionStudies");

            migrationBuilder.DropForeignKey(
                name: "FK_cotizacionStudies_Relacion_Solicitud_Paquete_PaqueteId",
                table: "cotizacionStudies");

            migrationBuilder.DropIndex(
                name: "IX_cotizacionStudies_CotizacionId",
                table: "cotizacionStudies");

            migrationBuilder.DropIndex(
                name: "IX_cotizacionStudies_PaqueteId",
                table: "cotizacionStudies");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Cotizaciones_CompañiaId",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Cotizaciones_MedicoId",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Cotizaciones_SucursalId",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropColumn(
                name: "Dias",
                table: "cotizacionStudies");

            migrationBuilder.DropColumn(
                name: "Horas",
                table: "cotizacionStudies");

            migrationBuilder.DropColumn(
                name: "ListaPrecio",
                table: "cotizacionStudies");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "cotizacionStudies");

            migrationBuilder.DropColumn(
                name: "CompañiaId",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropColumn(
                name: "SucursalId",
                table: "CAT_Cotizaciones");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cotizacionStudies",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "AplicaCargo",
                table: "cotizacionStudies",
                newName: "Descuento");

            migrationBuilder.RenameColumn(
                name: "FechaNacimiento",
                table: "CAT_Cotizaciones",
                newName: "FechaNac");

            migrationBuilder.RenameColumn(
                name: "EnvioWhatsapp",
                table: "CAT_Cotizaciones",
                newName: "Whatsapp");

            migrationBuilder.RenameColumn(
                name: "EnvioCorreo",
                table: "CAT_Cotizaciones",
                newName: "Correo");

            migrationBuilder.RenameColumn(
                name: "Clave",
                table: "CAT_Cotizaciones",
                newName: "Afiliacion");

            migrationBuilder.AlterColumn<int>(
                name: "EstudioId",
                table: "cotizacionStudies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Cargo",
                table: "cotizacionStudies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Copago",
                table: "cotizacionStudies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte>(
                name: "EstatusId",
                table: "cotizacionStudies",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<Guid>(
                name: "PriceQuoteId",
                table: "cotizacionStudies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PromocionId",
                table: "cotizacionStudies",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MedicoId",
                table: "CAT_Cotizaciones",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompaniaId",
                table: "CAT_Cotizaciones",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
