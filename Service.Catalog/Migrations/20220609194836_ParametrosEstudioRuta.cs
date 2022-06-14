using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class ParametrosEstudioRuta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
