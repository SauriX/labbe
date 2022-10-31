using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class AddValoresCriticosParameterValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriticoMaximo",
                table: "CAT_Parametro");

            migrationBuilder.DropColumn(
                name: "CriticoMinimo",
                table: "CAT_Parametro");

            migrationBuilder.DropColumn(
                name: "ValorCriticos",
                table: "CAT_Parametro");

            migrationBuilder.AddColumn<decimal>(
                name: "CriticoMaximoHombre",
                table: "CAT_Tipo_Valor",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CriticoMaximoMujer",
                table: "CAT_Tipo_Valor",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CriticoMinimoHombre",
                table: "CAT_Tipo_Valor",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CriticoMinimoMujer",
                table: "CAT_Tipo_Valor",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriticoMaximoHombre",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropColumn(
                name: "CriticoMaximoMujer",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropColumn(
                name: "CriticoMinimoHombre",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropColumn(
                name: "CriticoMinimoMujer",
                table: "CAT_Tipo_Valor");

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

            migrationBuilder.AddColumn<bool>(
                name: "ValorCriticos",
                table: "CAT_Parametro",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
