using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class addPrecioListaIdEnLoyalty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrecioListaId",
                table: "CAT_Lealtad",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PrecioListaId1",
                table: "CAT_Lealtad",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Lealtad_PrecioListaId1",
                table: "CAT_Lealtad",
                column: "PrecioListaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Lealtad_CAT_ListaPrecio_PrecioListaId1",
                table: "CAT_Lealtad",
                column: "PrecioListaId1",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Lealtad_CAT_ListaPrecio_PrecioListaId1",
                table: "CAT_Lealtad");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Lealtad_PrecioListaId1",
                table: "CAT_Lealtad");

            migrationBuilder.DropColumn(
                name: "PrecioListaId",
                table: "CAT_Lealtad");

            migrationBuilder.DropColumn(
                name: "PrecioListaId1",
                table: "CAT_Lealtad");
        }
    }
}
