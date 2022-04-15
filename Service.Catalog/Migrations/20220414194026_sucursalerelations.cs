using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class sucursalerelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Sucursal_CAT_Colonia_ColoniaId",
                table: "CAT_Sucursal");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Sucursal_ColoniaId",
                table: "CAT_Sucursal");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Sucursal_ColoniaId",
                table: "CAT_Sucursal",
                column: "ColoniaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Sucursal_CAT_Colonia_ColoniaId",
                table: "CAT_Sucursal",
                column: "ColoniaId",
                principalTable: "CAT_Colonia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Sucursal_CAT_Colonia_ColoniaId",
                table: "CAT_Sucursal");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Sucursal_ColoniaId",
                table: "CAT_Sucursal");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Sucursal_ColoniaId",
                table: "CAT_Sucursal",
                column: "ColoniaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Sucursal_CAT_Colonia_ColoniaId",
                table: "CAT_Sucursal",
                column: "ColoniaId",
                principalTable: "CAT_Colonia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
