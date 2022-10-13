using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class ModificacionWorkListStudyFechasUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreo",
                table: "Relacion_Estudio_WorkList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaMod",
                table: "Relacion_Estudio_WorkList",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_WorkList",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioModId",
                table: "Relacion_Estudio_WorkList",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreo",
                table: "Relacion_Estudio_WorkList");

            migrationBuilder.DropColumn(
                name: "FechaMod",
                table: "Relacion_Estudio_WorkList");

            migrationBuilder.DropColumn(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_WorkList");

            migrationBuilder.DropColumn(
                name: "UsuarioModId",
                table: "Relacion_Estudio_WorkList");
        }
    }
}
