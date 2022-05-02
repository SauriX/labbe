using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class SucursalDepartamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Sucursal_Departamento",
                columns: table => new
                {
                    SucursalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Sucursal_Departamento", x => new { x.SucursalId, x.DepartamentoId });
                    table.ForeignKey(
                        name: "FK_CAT_Sucursal_Departamento_CAT_Departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "CAT_Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_Sucursal_Departamento_CAT_Sucursal_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "CAT_Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Sucursal_Departamento_DepartamentoId",
                table: "CAT_Sucursal_Departamento",
                column: "DepartamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Sucursal_Departamento");
        }
    }
}
