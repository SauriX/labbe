using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CAT_Estudios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Estudio_Indicacion_Study_EstudioId",
                table: "Relacion_Estudio_Indicacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Study_CAT_Area_AreaId",
                table: "Study");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Study",
                table: "Study");

            migrationBuilder.RenameTable(
                name: "Study",
                newName: "CAT_Estudio");

            migrationBuilder.RenameIndex(
                name: "IX_Study_AreaId",
                table: "CAT_Estudio",
                newName: "IX_CAT_Estudio_AreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_Estudio",
                table: "CAT_Estudio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_Area_AreaId",
                table: "CAT_Estudio",
                column: "AreaId",
                principalTable: "CAT_Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Estudio_Indicacion_CAT_Estudio_EstudioId",
                table: "Relacion_Estudio_Indicacion",
                column: "EstudioId",
                principalTable: "CAT_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_Area_AreaId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Estudio_Indicacion_CAT_Estudio_EstudioId",
                table: "Relacion_Estudio_Indicacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_Estudio",
                table: "CAT_Estudio");

            migrationBuilder.RenameTable(
                name: "CAT_Estudio",
                newName: "Study");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Estudio_AreaId",
                table: "Study",
                newName: "IX_Study_AreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Study",
                table: "Study",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Estudio_Indicacion_Study_EstudioId",
                table: "Relacion_Estudio_Indicacion",
                column: "EstudioId",
                principalTable: "Study",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Study_CAT_Area_AreaId",
                table: "Study",
                column: "AreaId",
                principalTable: "CAT_Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
