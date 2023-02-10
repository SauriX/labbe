using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class update_params_history_delivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreo",
                table: "Historial_Envios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModifico",
                table: "Historial_Envios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCreoId",
                table: "Historial_Envios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioModificoId",
                table: "Historial_Envios",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreo",
                table: "Historial_Envios");

            migrationBuilder.DropColumn(
                name: "FechaModifico",
                table: "Historial_Envios");

            migrationBuilder.DropColumn(
                name: "UsuarioCreoId",
                table: "Historial_Envios");

            migrationBuilder.DropColumn(
                name: "UsuarioModificoId",
                table: "Historial_Envios");
        }
    }
}
