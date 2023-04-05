using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class ExtraSegRuta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestTagStudy_Relacion_Solicitud_Etiquetas_SolicitudEtiquetaId",
                table: "RequestTagStudy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestTagStudy",
                table: "RequestTagStudy");

            migrationBuilder.DropColumn(
                name: "ExpedienteId",
                table: "Relacion_Seguimiento_Solicitud");

            migrationBuilder.RenameTable(
                name: "RequestTagStudy",
                newName: "Relacion_Etiqueta_Estudio");

            migrationBuilder.RenameColumn(
                name: "NombrePaciente",
                table: "Relacion_Seguimiento_Solicitud",
                newName: "Clave");

            migrationBuilder.RenameColumn(
                name: "IsExtra",
                table: "Relacion_Seguimiento_Solicitud",
                newName: "Extra");

            migrationBuilder.RenameIndex(
                name: "IX_RequestTagStudy_SolicitudEtiquetaId",
                table: "Relacion_Etiqueta_Estudio",
                newName: "IX_Relacion_Etiqueta_Estudio_SolicitudEtiquetaId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioModId",
                table: "Relacion_Seguimiento_Solicitud",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaMod",
                table: "Relacion_Seguimiento_Solicitud",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relacion_Etiqueta_Estudio",
                table: "Relacion_Etiqueta_Estudio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Etiqueta_Estudio_Relacion_Solicitud_Etiquetas_SolicitudEtiquetaId",
                table: "Relacion_Etiqueta_Estudio",
                column: "SolicitudEtiquetaId",
                principalTable: "Relacion_Solicitud_Etiquetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Etiqueta_Estudio_Relacion_Solicitud_Etiquetas_SolicitudEtiquetaId",
                table: "Relacion_Etiqueta_Estudio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Relacion_Etiqueta_Estudio",
                table: "Relacion_Etiqueta_Estudio");

            migrationBuilder.RenameTable(
                name: "Relacion_Etiqueta_Estudio",
                newName: "RequestTagStudy");

            migrationBuilder.RenameColumn(
                name: "Extra",
                table: "Relacion_Seguimiento_Solicitud",
                newName: "IsExtra");

            migrationBuilder.RenameColumn(
                name: "Clave",
                table: "Relacion_Seguimiento_Solicitud",
                newName: "NombrePaciente");

            migrationBuilder.RenameIndex(
                name: "IX_Relacion_Etiqueta_Estudio_SolicitudEtiquetaId",
                table: "RequestTagStudy",
                newName: "IX_RequestTagStudy_SolicitudEtiquetaId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioModId",
                table: "Relacion_Seguimiento_Solicitud",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaMod",
                table: "Relacion_Seguimiento_Solicitud",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExpedienteId",
                table: "Relacion_Seguimiento_Solicitud",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestTagStudy",
                table: "RequestTagStudy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestTagStudy_Relacion_Solicitud_Etiquetas_SolicitudEtiquetaId",
                table: "RequestTagStudy",
                column: "SolicitudEtiquetaId",
                principalTable: "Relacion_Solicitud_Etiquetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
