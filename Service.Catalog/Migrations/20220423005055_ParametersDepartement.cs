using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class ParametersDepartement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartamentoId",
                table: "CAT_Parametro",
                newName: "DepartamentId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_DepartamentId",
                table: "CAT_Parametro",
                column: "DepartamentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_CAT_Departamento_DepartamentId",
                table: "CAT_Parametro",
                column: "DepartamentId",
                principalTable: "CAT_Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_CAT_Departamento_DepartamentId",
                table: "CAT_Parametro");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Parametro_DepartamentId",
                table: "CAT_Parametro");

            migrationBuilder.RenameColumn(
                name: "DepartamentId",
                table: "CAT_Parametro",
                newName: "DepartamentoId");
        }
    }
}
