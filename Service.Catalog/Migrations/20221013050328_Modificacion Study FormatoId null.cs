using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class ModificacionStudyFormatoIdnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_Cat_Formato_FormatoId",
                table: "CAT_Estudio");

            migrationBuilder.AlterColumn<int>(
                name: "FormatoId",
                table: "CAT_Estudio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_Cat_Formato_FormatoId",
                table: "CAT_Estudio",
                column: "FormatoId",
                principalTable: "Cat_Formato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_Cat_Formato_FormatoId",
                table: "CAT_Estudio");

            migrationBuilder.AlterColumn<int>(
                name: "FormatoId",
                table: "CAT_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_Cat_Formato_FormatoId",
                table: "CAT_Estudio",
                column: "FormatoId",
                principalTable: "Cat_Formato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
