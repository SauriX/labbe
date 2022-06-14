using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class DeletPromtionColumnCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Relacion_Estudio_Reactivo");

            migrationBuilder.DropColumn(
                name: "Clave",
                table: "Relacion_Estudio_Reactivo");

            migrationBuilder.DropColumn(
                name: "Departamento",
                table: "Relacion_Estudio_Reactivo");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Relacion_Estudio_Reactivo");

            migrationBuilder.DropColumn(
                name: "PromocionesId",
                table: "CAT_Compañia");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaMod",
                table: "Relacion_Ruta_Estudio",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaMod",
                table: "Relacion_Ruta_Estudio",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Relacion_Estudio_Reactivo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Clave",
                table: "Relacion_Estudio_Reactivo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Departamento",
                table: "Relacion_Estudio_Reactivo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Relacion_Estudio_Reactivo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PromocionesId",
                table: "CAT_Compañia",
                type: "bigint",
                nullable: true);
        }
    }
}
