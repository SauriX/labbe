using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class Eliminar_Columnas_AplicaDesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AplicaCargo",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropColumn(
                name: "AplicaCopago",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropColumn(
                name: "AplicaDescuento",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropColumn(
                name: "AplicaCargo",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "AplicaCopago",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "AplicaDescuento",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "CargoTipo",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "CopagoTipo",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "DescuentoTipo",
                table: "CAT_Solicitud");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AplicaCargo",
                table: "Relacion_Solicitud_Paquete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AplicaCopago",
                table: "Relacion_Solicitud_Paquete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AplicaDescuento",
                table: "Relacion_Solicitud_Paquete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AplicaCargo",
                table: "Relacion_Solicitud_Estudio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AplicaCopago",
                table: "Relacion_Solicitud_Estudio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AplicaDescuento",
                table: "Relacion_Solicitud_Estudio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte>(
                name: "CargoTipo",
                table: "CAT_Solicitud",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "CopagoTipo",
                table: "CAT_Solicitud",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "DescuentoTipo",
                table: "CAT_Solicitud",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
