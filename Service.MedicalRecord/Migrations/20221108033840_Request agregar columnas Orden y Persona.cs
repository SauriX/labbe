using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class RequestagregarcolumnasOrdenyPersona : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdOrden",
                table: "Relacion_Estudio_WeeClinic");

            migrationBuilder.AddColumn<string>(
                name: "IdOrden",
                table: "CAT_Solicitud",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdPersona",
                table: "CAT_Solicitud",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdOrden",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "IdPersona",
                table: "CAT_Solicitud");

            migrationBuilder.AddColumn<string>(
                name: "IdOrden",
                table: "Relacion_Estudio_WeeClinic",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
