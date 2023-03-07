using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class removetipostudio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<int>(
                name: "DiasEstabilidad",
                table: "CAT_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiasRefrigeracion",
                table: "CAT_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Instrucciones",
                table: "CAT_Estudio",
                type: "nvarchar(max)",
                nullable: true);
        }

    }
}
