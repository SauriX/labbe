using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Report.Migrations
{
    public partial class FechaReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Request",
                newName: "FechaInicial");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFinal",
                table: "Request",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaFinal",
                table: "Request");

            migrationBuilder.RenameColumn(
                name: "FechaInicial",
                table: "Request",
                newName: "Fecha");
        }
    }
}
