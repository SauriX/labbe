using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class FKUnidadesParametros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unidades",
                table: "CAT_Parametro",
                newName: "UnidadSiId");

            migrationBuilder.RenameColumn(
                name: "UnidadSi",
                table: "CAT_Parametro",
                newName: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_UnidadId",
                table: "CAT_Parametro",
                column: "UnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_UnidadSiId",
                table: "CAT_Parametro",
                column: "UnidadSiId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_CAT_Units_UnidadId",
                table: "CAT_Parametro",
                column: "UnidadId",
                principalTable: "CAT_Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_CAT_Units_UnidadSiId",
                table: "CAT_Parametro",
                column: "UnidadSiId",
                principalTable: "CAT_Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_CAT_Units_UnidadId",
                table: "CAT_Parametro");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_CAT_Units_UnidadSiId",
                table: "CAT_Parametro");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Parametro_UnidadId",
                table: "CAT_Parametro");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Parametro_UnidadSiId",
                table: "CAT_Parametro");

            migrationBuilder.RenameColumn(
                name: "UnidadSiId",
                table: "CAT_Parametro",
                newName: "Unidades");

            migrationBuilder.RenameColumn(
                name: "UnidadId",
                table: "CAT_Parametro",
                newName: "UnidadSi");
        }
    }
}
