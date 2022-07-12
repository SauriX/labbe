using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Report.Migrations
{
    public partial class TableSolicitudCambionombrecolumnafecha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaInicial",
                table: "Request",
                newName: "Fecha");

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Request",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Request");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Request",
                newName: "FechaInicial");
        }
    }
}
