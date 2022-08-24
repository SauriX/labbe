using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class mantainImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagen1",
                table: "CAT_Mantenimiento_Equipo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imagen2",
                table: "CAT_Mantenimiento_Equipo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen1",
                table: "CAT_Mantenimiento_Equipo");

            migrationBuilder.DropColumn(
                name: "Imagen2",
                table: "CAT_Mantenimiento_Equipo");
        }
    }
}
