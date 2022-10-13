using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class ModificacionReagentStudyFechasUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreo",
                table: "Relacion_Estudio_Reactivo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaMod",
                table: "Relacion_Estudio_Reactivo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_Reactivo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioModId",
                table: "Relacion_Estudio_Reactivo",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreo",
                table: "Relacion_Estudio_Reactivo");

            migrationBuilder.DropColumn(
                name: "FechaMod",
                table: "Relacion_Estudio_Reactivo");

            migrationBuilder.DropColumn(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_Reactivo");

            migrationBuilder.DropColumn(
                name: "UsuarioModId",
                table: "Relacion_Estudio_Reactivo");
        }
    }
}
