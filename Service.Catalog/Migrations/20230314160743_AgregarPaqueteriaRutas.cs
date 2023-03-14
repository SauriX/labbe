using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class AgregarPaqueteriaRutas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaqueteriaId",
                table: "CAT_Rutas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Rutas_PaqueteriaId",
                table: "CAT_Rutas",
                column: "PaqueteriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Rutas_CAT_Paqueteria_PaqueteriaId",
                table: "CAT_Rutas",
                column: "PaqueteriaId",
                principalTable: "CAT_Paqueteria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Rutas_CAT_Paqueteria_PaqueteriaId",
                table: "CAT_Rutas");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Rutas_PaqueteriaId",
                table: "CAT_Rutas");

            migrationBuilder.DropColumn(
                name: "PaqueteriaId",
                table: "CAT_Rutas");
        }
    }
}
