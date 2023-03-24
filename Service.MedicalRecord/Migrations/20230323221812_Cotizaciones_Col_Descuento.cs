using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class Cotizaciones_Col_Descuento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargoTipo",
                table: "CAT_Cotizacion");

            migrationBuilder.RenameColumn(
                name: "Cargo",
                table: "CAT_Cotizacion",
                newName: "Descuento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descuento",
                table: "CAT_Cotizacion",
                newName: "Cargo");

            migrationBuilder.AddColumn<byte>(
                name: "CargoTipo",
                table: "CAT_Cotizacion",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
