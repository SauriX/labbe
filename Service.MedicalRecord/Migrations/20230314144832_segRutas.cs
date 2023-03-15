using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class segRutas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Seguimiento_Solicitud_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "Relacion_Seguimiento_Solicitud");

            migrationBuilder.DropColumn(
                name: "Estudio",
                table: "Relacion_Seguimiento_Solicitud");

            migrationBuilder.DropColumn(
                name: "EstudioId",
                table: "Relacion_Seguimiento_Solicitud");

            migrationBuilder.RenameColumn(
                name: "Temperatura",
                table: "Relacion_Seguimiento_Solicitud",
                newName: "Cantidad");

            migrationBuilder.RenameColumn(
                name: "SolicitudEstudioId",
                table: "Relacion_Seguimiento_Solicitud",
                newName: "EtiquetaId");

            migrationBuilder.RenameIndex(
                name: "IX_Relacion_Seguimiento_Solicitud_SolicitudEstudioId",
                table: "Relacion_Seguimiento_Solicitud",
                newName: "IX_Relacion_Seguimiento_Solicitud_EtiquetaId");

            migrationBuilder.RenameColumn(
                name: "SucursalOrigenId",
                table: "CAT_Seguimiento_Ruta",
                newName: "OrigenId");

            migrationBuilder.RenameColumn(
                name: "SucursalDestinoId",
                table: "CAT_Seguimiento_Ruta",
                newName: "Muestra");

            migrationBuilder.RenameColumn(
                name: "MuestraId",
                table: "CAT_Seguimiento_Ruta",
                newName: "DestinoId");

            migrationBuilder.RenameColumn(
                name: "EscaneoCodigoBarras",
                table: "CAT_Seguimiento_Ruta",
                newName: "Escaneo");

            migrationBuilder.AlterColumn<Guid>(
                name: "DestinoId",
                table: "Relacion_Solicitud_Estudio",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "Temperatura",
                table: "CAT_Seguimiento_Ruta",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<Guid>(
                name: "RutaId",
                table: "CAT_Seguimiento_Ruta",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Seguimiento_Solicitud_Relacion_Solicitud_Etiquetas_EtiquetaId",
                table: "Relacion_Seguimiento_Solicitud",
                column: "EtiquetaId",
                principalTable: "Relacion_Solicitud_Etiquetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Seguimiento_Solicitud_Relacion_Solicitud_Etiquetas_EtiquetaId",
                table: "Relacion_Seguimiento_Solicitud");

            migrationBuilder.RenameColumn(
                name: "EtiquetaId",
                table: "Relacion_Seguimiento_Solicitud",
                newName: "SolicitudEstudioId");

            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "Relacion_Seguimiento_Solicitud",
                newName: "Temperatura");

            migrationBuilder.RenameIndex(
                name: "IX_Relacion_Seguimiento_Solicitud_EtiquetaId",
                table: "Relacion_Seguimiento_Solicitud",
                newName: "IX_Relacion_Seguimiento_Solicitud_SolicitudEstudioId");

            migrationBuilder.RenameColumn(
                name: "OrigenId",
                table: "CAT_Seguimiento_Ruta",
                newName: "SucursalOrigenId");

            migrationBuilder.RenameColumn(
                name: "Muestra",
                table: "CAT_Seguimiento_Ruta",
                newName: "SucursalDestinoId");

            migrationBuilder.RenameColumn(
                name: "Escaneo",
                table: "CAT_Seguimiento_Ruta",
                newName: "EscaneoCodigoBarras");

            migrationBuilder.RenameColumn(
                name: "DestinoId",
                table: "CAT_Seguimiento_Ruta",
                newName: "MuestraId");

            migrationBuilder.AlterColumn<Guid>(
                name: "DestinoId",
                table: "Relacion_Solicitud_Estudio",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estudio",
                table: "Relacion_Seguimiento_Solicitud",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstudioId",
                table: "Relacion_Seguimiento_Solicitud",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "Temperatura",
                table: "CAT_Seguimiento_Ruta",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "RutaId",
                table: "CAT_Seguimiento_Ruta",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Seguimiento_Solicitud_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "Relacion_Seguimiento_Solicitud",
                column: "SolicitudEstudioId",
                principalTable: "Relacion_Solicitud_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
