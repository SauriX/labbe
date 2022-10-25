using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class removeformat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_Cat_Formato_FormatoId",
                table: "CAT_Estudio");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Estudio_FormatoId",
                table: "CAT_Estudio");

            migrationBuilder.DropColumn(
                name: "FormatoId",
                table: "CAT_Estudio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormatoId",
                table: "CAT_Estudio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Estudio_FormatoId",
                table: "CAT_Estudio",
                column: "FormatoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_Cat_Formato_FormatoId",
                table: "CAT_Estudio",
                column: "FormatoId",
                principalTable: "Cat_Formato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
