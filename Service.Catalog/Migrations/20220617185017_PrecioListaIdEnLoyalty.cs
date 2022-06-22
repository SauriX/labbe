using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class PrecioListaIdEnLoyalty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrecioListaId",
                table: "CAT_Lealtad",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Lealtad_PrecioListaId",
                table: "CAT_Lealtad",
                column: "PrecioListaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Lealtad_CAT_ListaPrecio_PrecioListaId",
                table: "CAT_Lealtad",
                column: "PrecioListaId",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Lealtad_CAT_ListaPrecio_PrecioListaId",
                table: "CAT_Lealtad");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Lealtad_PrecioListaId",
                table: "CAT_Lealtad");

            migrationBuilder.DropColumn(
                name: "PrecioListaId",
                table: "CAT_Lealtad");
        }
    }
}
