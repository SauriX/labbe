using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class EliminarRelacionSucursalSerie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Sucursal_Serie");

            migrationBuilder.DropColumn(
                name: "Sucursal",
                table: "CAT_Serie");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Serie_SucursalId",
                table: "CAT_Serie",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Serie_CAT_Sucursal_SucursalId",
                table: "CAT_Serie",
                column: "SucursalId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Serie_CAT_Sucursal_SucursalId",
                table: "CAT_Serie");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Serie_SucursalId",
                table: "CAT_Serie");

            migrationBuilder.AddColumn<string>(
                name: "Sucursal",
                table: "CAT_Serie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Relacion_Sucursal_Serie",
                columns: table => new
                {
                    SucursalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerieId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
    }
}
