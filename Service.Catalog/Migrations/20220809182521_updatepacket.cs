using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class updatepacket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DescuenNum",
                table: "Relacion_ListaP_Paquete",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Descuento",
                table: "Relacion_ListaP_Paquete",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioFinal",
                table: "Relacion_ListaP_Paquete",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescuenNum",
                table: "Relacion_ListaP_Paquete");

            migrationBuilder.DropColumn(
                name: "Descuento",
                table: "Relacion_ListaP_Paquete");

            migrationBuilder.DropColumn(
                name: "PrecioFinal",
                table: "Relacion_ListaP_Paquete");
        }
    }
}
