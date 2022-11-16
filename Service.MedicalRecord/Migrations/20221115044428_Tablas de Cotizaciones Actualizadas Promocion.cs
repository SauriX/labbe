using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class TablasdeCotizacionesActualizadasPromocion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnvioWhatsapp",
                table: "CAT_Cotizacion",
                newName: "EnvioWhatsApp");

            migrationBuilder.AddColumn<decimal>(
                name: "Descuento",
                table: "Relacion_Cotizacion_Paquete",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DescuentoPorcentaje",
                table: "Relacion_Cotizacion_Paquete",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Promocion",
                table: "Relacion_Cotizacion_Paquete",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PromocionId",
                table: "Relacion_Cotizacion_Paquete",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Descuento",
                table: "Relacion_Cotizacion_Estudio",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DescuentoPorcentaje",
                table: "Relacion_Cotizacion_Estudio",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Promocion",
                table: "Relacion_Cotizacion_Estudio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PromocionId",
                table: "Relacion_Cotizacion_Estudio",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreo",
                table: "CAT_Cotizacion",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descuento",
                table: "Relacion_Cotizacion_Paquete");

            migrationBuilder.DropColumn(
                name: "DescuentoPorcentaje",
                table: "Relacion_Cotizacion_Paquete");

            migrationBuilder.DropColumn(
                name: "Promocion",
                table: "Relacion_Cotizacion_Paquete");

            migrationBuilder.DropColumn(
                name: "PromocionId",
                table: "Relacion_Cotizacion_Paquete");

            migrationBuilder.DropColumn(
                name: "Descuento",
                table: "Relacion_Cotizacion_Estudio");

            migrationBuilder.DropColumn(
                name: "DescuentoPorcentaje",
                table: "Relacion_Cotizacion_Estudio");

            migrationBuilder.DropColumn(
                name: "Promocion",
                table: "Relacion_Cotizacion_Estudio");

            migrationBuilder.DropColumn(
                name: "PromocionId",
                table: "Relacion_Cotizacion_Estudio");

            migrationBuilder.DropColumn(
                name: "UsuarioCreo",
                table: "CAT_Cotizacion");

            migrationBuilder.RenameColumn(
                name: "EnvioWhatsApp",
                table: "CAT_Cotizacion",
                newName: "EnvioWhatsapp");
        }
    }
}
