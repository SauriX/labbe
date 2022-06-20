using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class removcampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CiudadId",
                table: "CAT_Expedientes");

            migrationBuilder.DropColumn(
                name: "CodigoPostal",
                table: "CAT_Expedientes");

            migrationBuilder.DropColumn(
                name: "ColoniaId",
                table: "CAT_Expedientes");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "CAT_Expedientes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CiudadId",
                table: "CAT_Expedientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CodigoPostal",
                table: "CAT_Expedientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ColoniaId",
                table: "CAT_Expedientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "CAT_Expedientes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
