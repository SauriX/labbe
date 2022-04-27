using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class estudioparametro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Estudio_Parametro_CAT_Parametro_ParametersIdParametro",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Relacion_Estudio_Parametro",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Estudio_Parametro_ParametersIdParametro",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.DropColumn(
                name: "ParametersId",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.DropColumn(
                name: "ParametersIdParametro",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.AddColumn<Guid>(
                name: "ParametroId",
                table: "Relacion_Estudio_Parametro",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relacion_Estudio_Parametro",
                table: "Relacion_Estudio_Parametro",
                columns: new[] { "EstudioId", "ParametroId" });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_Parametro_ParametroId",
                table: "Relacion_Estudio_Parametro",
                column: "ParametroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Estudio_Parametro_CAT_Parametro_ParametroId",
                table: "Relacion_Estudio_Parametro",
                column: "ParametroId",
                principalTable: "CAT_Parametro",
                principalColumn: "IdParametro",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Estudio_Parametro_CAT_Parametro_ParametroId",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Relacion_Estudio_Parametro",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Estudio_Parametro_ParametroId",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.DropColumn(
                name: "ParametroId",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.AddColumn<string>(
                name: "ParametersId",
                table: "Relacion_Estudio_Parametro",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ParametersIdParametro",
                table: "Relacion_Estudio_Parametro",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relacion_Estudio_Parametro",
                table: "Relacion_Estudio_Parametro",
                columns: new[] { "EstudioId", "ParametersId" });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_Parametro_ParametersIdParametro",
                table: "Relacion_Estudio_Parametro",
                column: "ParametersIdParametro");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Estudio_Parametro_CAT_Parametro_ParametersIdParametro",
                table: "Relacion_Estudio_Parametro",
                column: "ParametersIdParametro",
                principalTable: "CAT_Parametro",
                principalColumn: "IdParametro",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
