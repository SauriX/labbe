using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Report.Migrations
{
    public partial class ChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Request",
                newName: "Sucursal");

            migrationBuilder.RenameColumn(
                name: "Clave",
                table: "Request",
                newName: "ExpedienteNombre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sucursal",
                table: "Request",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "ExpedienteNombre",
                table: "Request",
                newName: "Clave");
        }
    }
}
