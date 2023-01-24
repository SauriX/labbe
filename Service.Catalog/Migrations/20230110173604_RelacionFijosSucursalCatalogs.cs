using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class RelacionFijosSucursalCatalogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Presupuestos_CAT_Sucursal_SucursalId",
                table: "CAT_Presupuestos");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Presupuestos_SucursalId",
                table: "CAT_Presupuestos");

            migrationBuilder.DropColumn(
                name: "SucursalId",
                table: "CAT_Presupuestos");

            migrationBuilder.CreateTable(
                name: "Relacion_Presupuesto_Sucursal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SucursalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostoFijoId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Presupuesto_Sucursal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacion_Presupuesto_Sucursal_CAT_Presupuestos_CostoFijoId",
                        column: x => x.CostoFijoId,
                        principalTable: "CAT_Presupuestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Presupuesto_Sucursal_CAT_Sucursal_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "CAT_Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Presupuesto_Sucursal_CostoFijoId",
                table: "Relacion_Presupuesto_Sucursal",
                column: "CostoFijoId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Presupuesto_Sucursal_SucursalId",
                table: "Relacion_Presupuesto_Sucursal",
                column: "SucursalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Presupuesto_Sucursal");

            migrationBuilder.AddColumn<Guid>(
                name: "SucursalId",
                table: "CAT_Presupuestos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Presupuestos_SucursalId",
                table: "CAT_Presupuestos",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Presupuestos_CAT_Sucursal_SucursalId",
                table: "CAT_Presupuestos",
                column: "SucursalId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
