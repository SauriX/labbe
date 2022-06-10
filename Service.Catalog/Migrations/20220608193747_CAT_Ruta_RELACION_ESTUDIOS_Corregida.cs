using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CAT_Ruta_RELACION_ESTUDIOS_Corregida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FormatoDeTiempoId",
                table: "CAT_Rutas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Route_Study",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId1 = table.Column<int>(type: "int", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route_Study", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Route_Study_CAT_Estudio_EstudioId1",
                        column: x => x.EstudioId1,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Route_Study_CAT_Rutas_RouteId",
                        column: x => x.RouteId,
                        principalTable: "CAT_Rutas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Route_Study_EstudioId1",
                table: "Route_Study",
                column: "EstudioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Route_Study_RouteId",
                table: "Route_Study",
                column: "RouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Route_Study");

            migrationBuilder.DropColumn(
                name: "FormatoDeTiempoId",
                table: "CAT_Rutas");
        }
    }
}
