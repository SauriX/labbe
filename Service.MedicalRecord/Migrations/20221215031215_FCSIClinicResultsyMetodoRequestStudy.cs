using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class FCSIClinicResultsyMetodoRequestStudy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Metodo",
                table: "Relacion_Solicitud_Estudio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FCSI",
                table: "ClinicResults",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Metodo",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "FCSI",
                table: "ClinicResults");
        }
    }
}
