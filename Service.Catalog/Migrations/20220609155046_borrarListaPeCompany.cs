using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class borrarListaPeCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CAT_Compañia_CAT_ListaPrecio_ListaPrecioId1",
            //    table: "CAT_Compañia");

            //migrationBuilder.DropIndex(
            //    name: "IX_CAT_Compañia_ListaPrecioId1",
            //    table: "CAT_Compañia");

            migrationBuilder.DropColumn(
                name: "ListaPrecioId",
                table: "CAT_Compañia");

            //migrationBuilder.DropColumn(
            //    name: "ListaPrecioId1",
            //    table: "CAT_Compañia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListaPrecioId",
                table: "CAT_Compañia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ListaPrecioId1",
                table: "CAT_Compañia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Compañia_ListaPrecioId1",
                table: "CAT_Compañia",
                column: "ListaPrecioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Compañia_CAT_ListaPrecio_ListaPrecioId1",
                table: "CAT_Compañia",
                column: "ListaPrecioId1",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
