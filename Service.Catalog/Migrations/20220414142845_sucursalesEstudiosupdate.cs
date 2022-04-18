using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class sucursalesEstudiosupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Estudio_Sucursal_CAT_Estudio_EstudioId",
                table: "Relacion_Estudio_Sucursal");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Estudio_Sucursal_CAT_Sucursal_BranchId",
                table: "Relacion_Estudio_Sucursal");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Estudio_Sucursal_BranchId",
                table: "Relacion_Estudio_Sucursal");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Estudio_Sucursal_EstudioId",
                table: "Relacion_Estudio_Sucursal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_Sucursal_BranchId",
                table: "Relacion_Estudio_Sucursal",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_Sucursal_EstudioId",
                table: "Relacion_Estudio_Sucursal",
                column: "EstudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Estudio_Sucursal_CAT_Estudio_EstudioId",
                table: "Relacion_Estudio_Sucursal",
                column: "EstudioId",
                principalTable: "CAT_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Estudio_Sucursal_CAT_Sucursal_BranchId",
                table: "Relacion_Estudio_Sucursal",
                column: "BranchId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
