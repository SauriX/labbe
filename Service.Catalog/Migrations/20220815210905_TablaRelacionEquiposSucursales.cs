using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class TablaRelacionEquiposSucursales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relacion_Equipo_Sucursal",
                columns: table => new
                {
                    EquipmentBranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Num_Serie = table.Column<int>(type: "int", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Equipo_Sucursal", x => x.EquipmentBranchId);
                    table.ForeignKey(
                        name: "FK_Relacion_Equipo_Sucursal_CAT_Equipos_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "CAT_Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Equipo_Sucursal_CAT_Sucursal_BranchId",
                        column: x => x.BranchId,
                        principalTable: "CAT_Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Equipo_Sucursal_BranchId",
                table: "Relacion_Equipo_Sucursal",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Equipo_Sucursal_EquipmentId",
                table: "Relacion_Equipo_Sucursal",
                column: "EquipmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Equipo_Sucursal");
        }
    }
}
