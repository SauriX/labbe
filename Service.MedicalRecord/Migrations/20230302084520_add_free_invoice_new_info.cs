using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class add_free_invoice_new_info : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DireccionFiscal",
                table: "Factura_Compania",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RFC",
                table: "Factura_Compania",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazonSocial",
                table: "Factura_Compania",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegimenFiscal",
                table: "Factura_Compania",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DireccionFiscal",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "RFC",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "RazonSocial",
                table: "Factura_Compania");

            migrationBuilder.DropColumn(
                name: "RegimenFiscal",
                table: "Factura_Compania");
        }
    }
}
