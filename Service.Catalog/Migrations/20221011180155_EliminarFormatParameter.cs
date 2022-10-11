using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class EliminarFormatParameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_Cat_Formato_FormatoImpresionId",
                table: "CAT_Parametro");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Parametro_FormatoImpresionId",
                table: "CAT_Parametro");

            migrationBuilder.DropColumn(
                name: "FormatoImpresionId",
                table: "CAT_Parametro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormatoImpresionId",
                table: "CAT_Parametro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_FormatoImpresionId",
                table: "CAT_Parametro",
                column: "FormatoImpresionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_Cat_Formato_FormatoImpresionId",
                table: "CAT_Parametro",
                column: "FormatoImpresionId",
                principalTable: "Cat_Formato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
