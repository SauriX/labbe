using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class ModificacionPacketStudyFechasUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreo",
                table: "Relacion_Estudio_Paquete",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModifico",
                table: "Relacion_Estudio_Paquete",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_Paquete",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioModificoId",
                table: "Relacion_Estudio_Paquete",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreo",
                table: "Relacion_Estudio_Paquete");

            migrationBuilder.DropColumn(
                name: "FechaModifico",
                table: "Relacion_Estudio_Paquete");

            migrationBuilder.DropColumn(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_Paquete");

            migrationBuilder.DropColumn(
                name: "UsuarioModificoId",
                table: "Relacion_Estudio_Paquete");
        }
    }
}
