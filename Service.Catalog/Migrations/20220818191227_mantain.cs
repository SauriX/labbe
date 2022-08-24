using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class mantain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Mantenimiento_Equipo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha_Prog = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descrip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Num_Serie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EquipoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Mantenimiento_Equipo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Mantenimiento_Equipo_Relacion_Equipo_Sucursal_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Relacion_Equipo_Sucursal",
                        principalColumn: "EquipmentBranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Mantenimiento_Equipo_EquipoId",
                table: "CAT_Mantenimiento_Equipo",
                column: "EquipoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Mantenimiento_Equipo");
        }
    }
}
