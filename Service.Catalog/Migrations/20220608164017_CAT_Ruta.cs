using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CAT_Ruta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Rutas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SucursalOrigenId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maquilador = table.Column<bool>(type: "bit", nullable: true),
                    SucursalDestinoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaquiladorId = table.Column<int>(type: "int", nullable: true),
                    RequierePaqueteria = table.Column<bool>(type: "bit", nullable: true),
                    SeguimientoPaqueteria = table.Column<int>(type: "int", nullable: true),
                    PaqueteriaId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiasDeEntrega = table.Column<int>(type: "int", nullable: false),
                    HoraDeEntregaEstimada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoraDeEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoraDeRecoleccion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TiempoDeEntrega = table.Column<int>(type: "int", nullable: false),
                    FormatoDeTiempoId = table.Column<int>(type: "int", nullable: false),
                    EstudioId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaInicial = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdResponsableEnvio = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdResponsableRecepcion = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Lunes = table.Column<bool>(type: "bit", nullable: false),
                    Martes = table.Column<bool>(type: "bit", nullable: false),
                    Miercoles = table.Column<bool>(type: "bit", nullable: false),
                    Jueves = table.Column<bool>(type: "bit", nullable: false),
                    Viernes = table.Column<bool>(type: "bit", nullable: false),
                    Sabado = table.Column<bool>(type: "bit", nullable: false),
                    Domingo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Rutas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Ruta_Estudio",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    RutaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Ruta_Estudio", x => new { x.RouteId, x.EstudioId });
                    table.ForeignKey(
                        name: "FK_Relacion_Ruta_Estudio_CAT_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Ruta_Estudio_CAT_Rutas_RutaId",
                        column: x => x.RutaId,
                        principalTable: "CAT_Rutas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Ruta_Estudio_EstudioId",
                table: "Relacion_Ruta_Estudio",
                column: "EstudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Ruta_Estudio_RutaId",
                table: "Relacion_Ruta_Estudio",
                column: "RutaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Ruta_Estudio");

            migrationBuilder.DropTable(
                name: "CAT_Rutas");
        }
    }
}
