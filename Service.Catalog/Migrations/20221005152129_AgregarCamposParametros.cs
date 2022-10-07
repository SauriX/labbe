using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class AgregarCamposParametros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CriticoMaximo",
                table: "CAT_Parametro",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CriticoMinimo",
                table: "CAT_Parametro",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorFinal",
                table: "CAT_Parametro",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriticoMaximo",
                table: "CAT_Parametro");

            migrationBuilder.DropColumn(
                name: "CriticoMinimo",
                table: "CAT_Parametro");

            migrationBuilder.DropColumn(
                name: "ValorFinal",
                table: "CAT_Parametro");
        }
    }
}
