using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class Estudios_WeeClinic_FK_DEL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstudioWeeClinicId",
                table: "Relacion_Solicitud_Estudio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstudioWeeClinicId",
                table: "Relacion_Solicitud_Estudio",
                type: "int",
                nullable: true);
        }
    }
}
