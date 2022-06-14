using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CAT_Ruta_RELACION_ESTUDIOS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Ruta_Estudio");

            migrationBuilder.DropColumn(
                name: "FormatoDeTiempoId",
                table: "CAT_Rutas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormatoDeTiempoId",
                table: "CAT_Rutas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Relacion_Ruta_Estudio",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RutaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                        name: "FK_Relacion_Ruta_Estudio_CAT_Rutas_RutaId",
                        column: x => x.RutaId,
                        principalTable: "CAT_Rutas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Ruta_Estudio_EstudioId",
                table: "Relacion_Ruta_Estudio",
                column: "EstudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Ruta_Estudio_RutaId",
                table: "Relacion_Ruta_Estudio",
                column: "RutaId");
        }
    }
}
