using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class add_free_invoice_info : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClaveProdServ",
                table: "Relacion_Factura_Detalle",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrigenFactura",
                table: "Factura_Compania",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaveProdServ",
                table: "Relacion_Factura_Detalle");

            migrationBuilder.DropColumn(
                name: "OrigenFactura",
                table: "Factura_Compania");
        }
    }
}
