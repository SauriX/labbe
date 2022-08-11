using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class CAT_SolicitudTotales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cargo",
                table: "CAT_Solicitud",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<byte>(
                name: "CargoTipo",
                table: "CAT_Solicitud",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<decimal>(
                name: "Copago",
                table: "CAT_Solicitud",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<byte>(
                name: "CopagoTipo",
                table: "CAT_Solicitud",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<decimal>(
                name: "Descuento",
                table: "CAT_Solicitud",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<byte>(
                name: "DescuentoTipo",
                table: "CAT_Solicitud",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<decimal>(
                name: "Saldo",
                table: "CAT_Solicitud",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "CAT_Solicitud",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalEstudios",
                table: "CAT_Solicitud",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cargo",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "CargoTipo",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "Copago",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "CopagoTipo",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "Descuento",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "DescuentoTipo",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "TotalEstudios",
                table: "CAT_Solicitud");
        }
    }
}
