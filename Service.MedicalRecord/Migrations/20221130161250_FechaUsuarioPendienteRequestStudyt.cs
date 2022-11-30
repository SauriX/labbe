using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class FechaUsuarioPendienteRequestStudyt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPendiente",
                table: "Relacion_Solicitud_Estudio",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioPendiente",
                table: "Relacion_Solicitud_Estudio",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaPendiente",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "UsuarioPendiente",
                table: "Relacion_Solicitud_Estudio");
        }
    }
}
