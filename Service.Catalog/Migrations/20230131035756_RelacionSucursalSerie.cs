using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class RelacionSucursalSerie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relacion_Sucursal_Serie",
                columns: table => new
                {
                    SucursalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerieId = table.Column<int>(type: "int", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Sucursal_Serie", x => new { x.SucursalId, x.SerieId });
                    table.ForeignKey(
                        name: "FK_Relacion_Sucursal_Serie_CAT_Serie_SerieId",
                        column: x => x.SerieId,
                        principalTable: "CAT_Serie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Sucursal_Serie_CAT_Sucursal_BranchId",
                        column: x => x.BranchId,
                        principalTable: "CAT_Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relacion_Sucursal_Serie_CAT_Sucursal_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "CAT_Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Sucursal_Serie_BranchId",
                table: "Relacion_Sucursal_Serie",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Sucursal_Serie_SerieId",
                table: "Relacion_Sucursal_Serie",
                column: "SerieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Sucursal_Serie");
        }
    }
}
