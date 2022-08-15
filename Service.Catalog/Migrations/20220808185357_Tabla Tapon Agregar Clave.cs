using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class TablaTaponAgregarClave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CAT_Tipo_Tapon",
                newName: "Nombre");

            migrationBuilder.AddColumn<string>(
                name: "Clave",
                table: "CAT_Tipo_Tapon",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clave",
                table: "CAT_Tipo_Tapon");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "CAT_Tipo_Tapon",
                newName: "Name");
        }
    }
}
