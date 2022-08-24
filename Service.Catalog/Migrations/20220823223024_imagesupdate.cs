using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class imagesupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Mantenimiento_Equipo_Images_CAT_Mantenimiento_Equipo_Id",
                table: "CAT_Mantenimiento_Equipo_Images");

            migrationBuilder.AddColumn<Guid>(
                name: "MantainId1",
                table: "CAT_Mantenimiento_Equipo_Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Mantenimiento_Equipo_Images_MantainId1",
                table: "CAT_Mantenimiento_Equipo_Images",
                column: "MantainId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Mantenimiento_Equipo_Images_CAT_Mantenimiento_Equipo_MantainId1",
                table: "CAT_Mantenimiento_Equipo_Images",
                column: "MantainId1",
                principalTable: "CAT_Mantenimiento_Equipo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Mantenimiento_Equipo_Images_CAT_Mantenimiento_Equipo_MantainId1",
                table: "CAT_Mantenimiento_Equipo_Images");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Mantenimiento_Equipo_Images_MantainId1",
                table: "CAT_Mantenimiento_Equipo_Images");

            migrationBuilder.DropColumn(
                name: "MantainId1",
                table: "CAT_Mantenimiento_Equipo_Images");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Mantenimiento_Equipo_Images_CAT_Mantenimiento_Equipo_Id",
                table: "CAT_Mantenimiento_Equipo_Images",
                column: "Id",
                principalTable: "CAT_Mantenimiento_Equipo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
