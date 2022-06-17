    using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class removetelefono : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroExterior",
                table: "CAT_Expedientes");

            migrationBuilder.DropColumn(
                name: "NumeroInterior",
                table: "CAT_Expedientes");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "CAT_Expedientes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumeroExterior",
                table: "CAT_Expedientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroInterior",
                table: "CAT_Expedientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Telefono",
                table: "CAT_Expedientes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
