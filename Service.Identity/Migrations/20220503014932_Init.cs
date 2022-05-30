using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Identity.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Menu",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuPadreId = table.Column<short>(type: "smallint", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Controlador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ruta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orden = table.Column<short>(type: "smallint", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Menu_CAT_Menu_MenuPadreId",
                        column: x => x.MenuPadreId,
                        principalTable: "CAT_Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Rol",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Rol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Rol_Permiso",
                columns: table => new
                {
                    RolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuId = table.Column<short>(type: "smallint", nullable: false),
                    Acceder = table.Column<bool>(type: "bit", nullable: false),
                    Crear = table.Column<bool>(type: "bit", nullable: false),
                    Modificar = table.Column<bool>(type: "bit", nullable: false),
                    Imprimir = table.Column<bool>(type: "bit", nullable: false),
                    Descargar = table.Column<bool>(type: "bit", nullable: false),
                    EnviarCorreo = table.Column<bool>(type: "bit", nullable: false),
                    EnviarWapp = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Rol_Permiso", x => new { x.RolId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_CAT_Rol_Permiso_CAT_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "CAT_Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_Rol_Permiso_CAT_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "CAT_Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimerApellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundoApellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SucursalId = table.Column<int>(type: "int", nullable: true),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FlagPassword = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Usuario_CAT_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "CAT_Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Usuario_Permiso",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuId = table.Column<short>(type: "smallint", nullable: false),
                    Acceder = table.Column<bool>(type: "bit", nullable: false),
                    Crear = table.Column<bool>(type: "bit", nullable: false),
                    Modificar = table.Column<bool>(type: "bit", nullable: false),
                    Imprimir = table.Column<bool>(type: "bit", nullable: false),
                    Descargar = table.Column<bool>(type: "bit", nullable: false),
                    EnviarCorreo = table.Column<bool>(type: "bit", nullable: false),
                    EnviarWapp = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Usuario_Permiso", x => new { x.UsuarioId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_CAT_Usuario_Permiso_CAT_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "CAT_Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_Usuario_Permiso_CAT_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "CAT_Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Menu_MenuPadreId",
                table: "CAT_Menu",
                column: "MenuPadreId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Rol_Permiso_MenuId",
                table: "CAT_Rol_Permiso",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Usuario_RolId",
                table: "CAT_Usuario",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Usuario_Permiso_MenuId",
                table: "CAT_Usuario_Permiso",
                column: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Rol_Permiso");

            migrationBuilder.DropTable(
                name: "CAT_Usuario_Permiso");

            migrationBuilder.DropTable(
                name: "CAT_Menu");

            migrationBuilder.DropTable(
                name: "CAT_Usuario");

            migrationBuilder.DropTable(
                name: "CAT_Rol");
        }
    }
}
