using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class ValoresporColumnaParameterValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CuartaColumna",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimeraColumna",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuintaColumna",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SegundaColumna",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TerceraColumna",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CuartaColumna",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropColumn(
                name: "PrimeraColumna",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropColumn(
                name: "QuintaColumna",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropColumn(
                name: "SegundaColumna",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropColumn(
                name: "TerceraColumna",
                table: "CAT_Tipo_Valor");
        }
    }
}
