using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class EliminarHoraRecoleccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraDeRecoleccion",
                table: "CAT_Rutas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HoraDeRecoleccion",
                table: "CAT_Rutas",
                type: "int",
                nullable: true);
        }
    }
}
