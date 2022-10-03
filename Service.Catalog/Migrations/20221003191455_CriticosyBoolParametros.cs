using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CriticosyBoolParametros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CriticoMaximo",
                table: "CAT_Tipo_Valor",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CriticoMinimo",
                table: "CAT_Tipo_Valor",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "DeltaCheck",
                table: "CAT_Parametro",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MostrarFormato",
                table: "CAT_Parametro",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ValorCriticos",
                table: "CAT_Parametro",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriticoMaximo",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropColumn(
                name: "CriticoMinimo",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropColumn(
                name: "DeltaCheck",
                table: "CAT_Parametro");

            migrationBuilder.DropColumn(
                name: "MostrarFormato",
                table: "CAT_Parametro");

            migrationBuilder.DropColumn(
                name: "ValorCriticos",
                table: "CAT_Parametro");
        }
    }
}
