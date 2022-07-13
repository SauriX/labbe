using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Report.Migrations
{
    public partial class ChangeNameRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sucursal",
                table: "Request",
                newName: "PacienteNombre");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Request",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PacienteNombre",
                table: "Request",
                newName: "Sucursal");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Request",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }
    }
}
