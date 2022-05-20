using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class idMaperPrecios : Migration
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
