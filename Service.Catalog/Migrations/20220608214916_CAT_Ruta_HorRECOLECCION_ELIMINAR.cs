using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CAT_Ruta_HorRECOLECCION_ELIMINAR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraDeRecoleccion",
                table: "CAT_Rutas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "HoraDeRecoleccion",
                table: "CAT_Rutas",
                type: "datetime2",
                nullable: true);
        }
    }
}
