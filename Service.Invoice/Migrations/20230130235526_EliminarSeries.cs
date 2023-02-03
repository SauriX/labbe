using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Billing.Migrations
{
    public partial class EliminarSeries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Serie");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Serie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    ArchivoCer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArchivoKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CFDI = table.Column<bool>(type: "bit", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmisorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sucursal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SucursalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SucursalKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoSerie = table.Column<byte>(type: "tinyint", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Serie", x => x.Id);
                });
        }
    }
}
