using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class StringtoDecimalCriticosClinicResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CriticoMaximo",
                table: "ClinicResults",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CriticoMinimo",
                table: "ClinicResults",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriticoMaximo",
                table: "ClinicResults");

            migrationBuilder.DropColumn(
                name: "CriticoMinimo",
                table: "ClinicResults");
        }
    }
}
