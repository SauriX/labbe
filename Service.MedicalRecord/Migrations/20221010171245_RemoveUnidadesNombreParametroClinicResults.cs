using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class RemoveUnidadesNombreParametroClinicResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreParametro",
                table: "ClinicResults");

            migrationBuilder.DropColumn(
                name: "Unidades",
                table: "ClinicResults");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreParametro",
                table: "ClinicResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unidades",
                table: "ClinicResults",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
