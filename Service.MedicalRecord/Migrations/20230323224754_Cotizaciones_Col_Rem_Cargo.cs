using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class Cotizaciones_Col_Rem_Cargo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AplicaCargo",
                table: "Relacion_Cotizacion_Paquete");

            migrationBuilder.DropColumn(
                name: "AplicaCargo",
                table: "Relacion_Cotizacion_Estudio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AplicaCargo",
                table: "Relacion_Cotizacion_Paquete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AplicaCargo",
                table: "Relacion_Cotizacion_Estudio",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
