using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class removediasresultado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiasResultado",
                table: "CAT_Estudio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiasResultado",
                table: "CAT_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
