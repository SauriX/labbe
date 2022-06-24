using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class taxdatacgangedatatipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calle",
                table: "CAT_Datos_Fiscales");

            migrationBuilder.DropColumn(
                name: "CiudadId",
                table: "CAT_Datos_Fiscales");

            migrationBuilder.DropColumn(
                name: "CodigoPostal",
                table: "CAT_Datos_Fiscales");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "CAT_Datos_Fiscales");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "CAT_Datos_Fiscales");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Calle",
                table: "CAT_Datos_Fiscales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CiudadId",
                table: "CAT_Datos_Fiscales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CodigoPostal",
                table: "CAT_Datos_Fiscales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "CAT_Datos_Fiscales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "CAT_Datos_Fiscales",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
