using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class add_company_form_values : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BancoId",
                table: "Factura_Compania",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ClaveExterna",
                table: "Factura_Compania",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiasCredito",
                table: "Factura_Compania",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FormaPagoId",
                table: "Factura_Compania",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoPago",
                table: "Factura_Compania",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BancoId",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "ClaveExterna",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "DiasCredito",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "FormaPagoId",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "TipoPago",
                table: "Factura_Compania");
        }
    }
}
