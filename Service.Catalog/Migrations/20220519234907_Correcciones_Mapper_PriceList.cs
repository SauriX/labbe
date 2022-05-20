using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class Correcciones_Mapper_PriceList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Relacion_ListaP_Estudio");

            migrationBuilder.DropColumn(
                name: "Clave",
                table: "Relacion_ListaP_Estudio");

            migrationBuilder.DropColumn(
                name: "Departamento",
                table: "Relacion_ListaP_Estudio");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Relacion_ListaP_Estudio");

            migrationBuilder.DropColumn(
                name: "Clave",
                table: "CAT_ListaP_Sucursal");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "CAT_ListaP_Sucursal");

            migrationBuilder.DropColumn(
                name: "Clave",
                table: "CAT_ListaP_Medicos");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "CAT_ListaP_Medicos");

            migrationBuilder.DropColumn(
                name: "Clave",
                table: "CAT_ListaP_Compañia");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "CAT_ListaP_Compañia");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioCreoId",
                table: "CAT_ListaP_Sucursal",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Relacion_ListaP_Estudio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Clave",
                table: "Relacion_ListaP_Estudio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Departamento",
                table: "Relacion_ListaP_Estudio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Relacion_ListaP_Estudio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioCreoId",
                table: "CAT_ListaP_Sucursal",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Clave",
                table: "CAT_ListaP_Sucursal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "CAT_ListaP_Sucursal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Clave",
                table: "CAT_ListaP_Medicos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "CAT_ListaP_Medicos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Clave",
                table: "CAT_ListaP_Compañia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "CAT_ListaP_Compañia",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
