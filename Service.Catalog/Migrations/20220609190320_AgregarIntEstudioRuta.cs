using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class AgregarIntEstudioRuta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relacion_Ruta_Estudio",
                columns: table => new
                {
                    RouteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Ruta_Estudio", x => new { x.RouteId, x.EstudioId });
                    table.ForeignKey(
                        name: "FK_Relacion_Ruta_Estudio_CAT_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Ruta_Estudio_CAT_Rutas_RouteId",
                        column: x => x.RouteId,
                        principalTable: "CAT_Rutas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Ruta_Estudio_EstudioId",
                table: "Relacion_Ruta_Estudio",
                column: "EstudioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Ruta_Estudio");
        }
    }
}
