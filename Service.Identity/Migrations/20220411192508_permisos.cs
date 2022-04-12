using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Identity.Migrations
{
    public partial class permisos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Permisos",
                columns: table => new
                {
                    IdPermiso = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Acceso = table.Column<bool>(type: "bit", nullable: false),
                    Crear = table.Column<bool>(type: "bit", nullable: false),
                    Modificación = table.Column<bool>(type: "bit", nullable: false),
                    Impresión = table.Column<bool>(type: "bit", nullable: false),
                    Descarga = table.Column<bool>(type: "bit", nullable: false),
                    EnvioCorreo = table.Column<bool>(type: "bit", nullable: false),
                    EnvioWapp = table.Column<bool>(type: "bit", nullable: false),
                    RolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmoduloId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Permisos", x => x.IdPermiso);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Permisos");
        }
    }
}
