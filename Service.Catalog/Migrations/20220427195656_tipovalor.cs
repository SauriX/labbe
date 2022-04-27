using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class tipovalor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Tipo_Valor_CAT_Parametro_parametresIdParametro",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Tipo_Valor_parametresIdParametro",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropColumn(
                name: "parametresIdParametro",
                table: "CAT_Tipo_Valor");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Tipo_Valor_IdParametro",
                table: "CAT_Tipo_Valor",
                column: "IdParametro");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Tipo_Valor_CAT_Parametro_IdParametro",
                table: "CAT_Tipo_Valor",
                column: "IdParametro",
                principalTable: "CAT_Parametro",
                principalColumn: "IdParametro",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Tipo_Valor_CAT_Parametro_IdParametro",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Tipo_Valor_IdParametro",
                table: "CAT_Tipo_Valor");

            migrationBuilder.AddColumn<Guid>(
                name: "parametresIdParametro",
                table: "CAT_Tipo_Valor",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Tipo_Valor_parametresIdParametro",
                table: "CAT_Tipo_Valor",
                column: "parametresIdParametro");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Tipo_Valor_CAT_Parametro_parametresIdParametro",
                table: "CAT_Tipo_Valor",
                column: "parametresIdParametro",
                principalTable: "CAT_Parametro",
                principalColumn: "IdParametro",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
