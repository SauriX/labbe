using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class sucusalubicacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "CAT_Sucursal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Codigopostal",
                table: "CAT_Sucursal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "CAT_Sucursal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "colony",
                table: "CAT_Sucursal",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "CAT_Sucursal");

            migrationBuilder.DropColumn(
                name: "Codigopostal",
                table: "CAT_Sucursal");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "CAT_Sucursal");

            migrationBuilder.DropColumn(
                name: "colony",
                table: "CAT_Sucursal");
        }
    }
}
