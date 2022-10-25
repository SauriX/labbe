using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class Solicitud_Pago_Serie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "Relacion_Solicitud_Pago",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Serie",
                table: "Relacion_Solicitud_Pago",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Relacion_Solicitud_Pago");

            migrationBuilder.DropColumn(
                name: "Serie",
                table: "Relacion_Solicitud_Pago");
        }
    }
}
