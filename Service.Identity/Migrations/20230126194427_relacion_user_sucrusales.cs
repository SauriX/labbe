using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Identity.Migrations
{
    public partial class relacion_user_sucrusales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relacion_User_Sucursal",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_User_Sucursal", x => new { x.UsuarioId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_Relacion_User_Sucursal_CAT_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "CAT_Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_User_Sucursal");
        }
    }
}
