using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class SucursalEstudioT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Sucursal_Departamento_CAT_Sucursal_BranchId",
                table: "CAT_Sucursal_Departamento");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Sucursal_Departamento_BranchId",
                table: "CAT_Sucursal_Departamento");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "CAT_Sucursal_Departamento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "CAT_Sucursal_Departamento",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Sucursal_Departamento_BranchId",
                table: "CAT_Sucursal_Departamento",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Sucursal_Departamento_CAT_Sucursal_BranchId",
                table: "CAT_Sucursal_Departamento",
                column: "BranchId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
