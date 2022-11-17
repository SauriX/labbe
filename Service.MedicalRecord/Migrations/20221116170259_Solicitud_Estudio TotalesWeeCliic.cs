using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class Solicitud_EstudioTotalesWeeCliic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalAseguradora",
                table: "Relacion_Estudio_WeeClinic",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPaciente",
                table: "Relacion_Estudio_WeeClinic",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAseguradora",
                table: "Relacion_Estudio_WeeClinic");

            migrationBuilder.DropColumn(
                name: "TotalPaciente",
                table: "Relacion_Estudio_WeeClinic");
        }
    }
}
