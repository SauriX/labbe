using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class removeimagen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Mantenimiento_Equipo_Images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Mantenimiento_Equipo_Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MantainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UrlImg = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Mantenimiento_Equipo_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Mantenimiento_Equipo_Images_CAT_Mantenimiento_Equipo_MantainId",
                        column: x => x.MantainId,
                        principalTable: "CAT_Mantenimiento_Equipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Mantenimiento_Equipo_Images_MantainId",
                table: "CAT_Mantenimiento_Equipo_Images",
                column: "MantainId");
        }
    }
}
