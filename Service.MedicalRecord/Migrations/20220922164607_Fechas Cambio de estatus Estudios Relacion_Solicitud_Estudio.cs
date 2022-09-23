using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class FechasCambiodeestatusEstudiosRelacion_Solicitud_Estudio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCaptura",
                table: "Relacion_Solicitud_Estudio",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEnviado",
                table: "Relacion_Solicitud_Estudio",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaLiberado",
                table: "Relacion_Solicitud_Estudio",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSolicitado",
                table: "Relacion_Solicitud_Estudio",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaTomaMuestra",
                table: "Relacion_Solicitud_Estudio",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaValidacion",
                table: "Relacion_Solicitud_Estudio",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCaptura",
                table: "Relacion_Solicitud_Estudio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioEnviado",
                table: "Relacion_Solicitud_Estudio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioLiberado",
                table: "Relacion_Solicitud_Estudio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioSolicitado",
                table: "Relacion_Solicitud_Estudio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioTomaMuestra",
                table: "Relacion_Solicitud_Estudio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioValidacion",
                table: "Relacion_Solicitud_Estudio",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCaptura",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "FechaEnviado",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "FechaLiberado",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "FechaSolicitado",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "FechaTomaMuestra",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "FechaValidacion",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "UsuarioCaptura",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "UsuarioEnviado",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "UsuarioLiberado",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "UsuarioSolicitado",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "UsuarioTomaMuestra",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "UsuarioValidacion",
                table: "Relacion_Solicitud_Estudio");
        }
    }
}
