using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class Precios_Paquetes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_Etiqueta_TaponId",
                table: "CAT_Estudio");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Estudio_TaponId",
                table: "CAT_Estudio");

            migrationBuilder.DropColumn(
                name: "TaponId",
                table: "CAT_Estudio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaponId",
                table: "CAT_Estudio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Estudio_TaponId",
                table: "CAT_Estudio",
                column: "TaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_Etiqueta_TaponId",
                table: "CAT_Estudio",
                column: "TaponId",
                principalTable: "CAT_Etiqueta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
