using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class DecimalValoresIFClinicResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParameterId",
                table: "CAT_Tipo_Valor",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Tipo_Valor_ParameterId",
                table: "CAT_Tipo_Valor",
                column: "ParameterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Tipo_Valor_CAT_Parametro_ParameterId",
                table: "CAT_Tipo_Valor",
                column: "ParameterId",
                principalTable: "CAT_Parametro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Tipo_Valor_CAT_Parametro_ParameterId",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Tipo_Valor_ParameterId",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropColumn(
                name: "ParameterId",
                table: "CAT_Tipo_Valor");
        }
    }
}
