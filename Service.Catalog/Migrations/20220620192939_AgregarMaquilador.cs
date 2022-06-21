using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class AgregarMaquilador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CAT_Rutas_MaquiladorId",
                table: "CAT_Rutas",
                column: "MaquiladorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Rutas_CAT_Maquilador_MaquiladorId",
                table: "CAT_Rutas",
                column: "MaquiladorId",
                principalTable: "CAT_Maquilador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Rutas_CAT_Maquilador_MaquiladorId",
                table: "CAT_Rutas");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Rutas_MaquiladorId",
                table: "CAT_Rutas");
        }
    }
}
