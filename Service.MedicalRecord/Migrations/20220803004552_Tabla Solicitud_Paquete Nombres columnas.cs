using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class TablaSolicitud_PaqueteNombrescolumnas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Copago",
                table: "Relacion_Solicitud_Paquete",
                newName: "AplicaDescuento");

            migrationBuilder.RenameColumn(
                name: "Cargo",
                table: "Relacion_Solicitud_Paquete",
                newName: "AplicaCopago");

            migrationBuilder.RenameColumn(
                name: "Copago",
                table: "Relacion_Solicitud_Estudio",
                newName: "AplicaDescuento");

            migrationBuilder.RenameColumn(
                name: "Cargo",
                table: "Relacion_Solicitud_Estudio",
                newName: "AplicaCopago");

            migrationBuilder.AlterColumn<decimal>(
                name: "Descuento",
                table: "Relacion_Solicitud_Paquete",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "AplicaCargo",
                table: "Relacion_Solicitud_Paquete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Dias",
                table: "Relacion_Solicitud_Paquete",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Horas",
                table: "Relacion_Solicitud_Paquete",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Descuento",
                table: "Relacion_Solicitud_Estudio",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "AplicaCargo",
                table: "Relacion_Solicitud_Estudio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Dias",
                table: "Relacion_Solicitud_Estudio",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Horas",
                table: "Relacion_Solicitud_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AplicaCargo",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropColumn(
                name: "Dias",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropColumn(
                name: "Horas",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropColumn(
                name: "AplicaCargo",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "Dias",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "Horas",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.RenameColumn(
                name: "AplicaDescuento",
                table: "Relacion_Solicitud_Paquete",
                newName: "Copago");

            migrationBuilder.RenameColumn(
                name: "AplicaCopago",
                table: "Relacion_Solicitud_Paquete",
                newName: "Cargo");

            migrationBuilder.RenameColumn(
                name: "AplicaDescuento",
                table: "Relacion_Solicitud_Estudio",
                newName: "Copago");

            migrationBuilder.RenameColumn(
                name: "AplicaCopago",
                table: "Relacion_Solicitud_Estudio",
                newName: "Cargo");

            migrationBuilder.AlterColumn<bool>(
                name: "Descuento",
                table: "Relacion_Solicitud_Paquete",
                type: "bit",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<bool>(
                name: "Descuento",
                table: "Relacion_Solicitud_Estudio",
                type: "bit",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
