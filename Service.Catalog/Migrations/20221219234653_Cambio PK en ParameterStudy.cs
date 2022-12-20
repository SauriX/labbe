using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CambioPKenParameterStudy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Relacion_Estudio_Parametro",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Relacion_Estudio_Parametro",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relacion_Estudio_Parametro",
                table: "Relacion_Estudio_Parametro",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_Parametro_EstudioId",
                table: "Relacion_Estudio_Parametro",
                column: "EstudioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Relacion_Estudio_Parametro",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Estudio_Parametro_EstudioId",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relacion_Estudio_Parametro",
                table: "Relacion_Estudio_Parametro",
                columns: new[] { "EstudioId", "ParametroId" });
        }
    }
}
