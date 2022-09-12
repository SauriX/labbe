using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class Tablas_CAT_Seguimiento_Ruta_y_Relacion_Seguimiento_Solicitud : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Seguimiento_Ruta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SucursalDestinoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SucursalOrigenId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaquiladorId = table.Column<int>(type: "int", nullable: false),
                    RutaId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaRecoleccion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MuestraId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EscaneoCodigoBarras = table.Column<bool>(type: "bit", nullable: false),
                    Temperatura = table.Column<double>(type: "float", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Seguimiento_Ruta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Seguimiento_Solicitud",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeguimientoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estudio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpedienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombrePaciente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temperatura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Escaneado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Seguimiento_Solicitud", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacion_Seguimiento_Solicitud_CAT_Seguimiento_Ruta_SeguimientoId",
                        column: x => x.SeguimientoId,
                        principalTable: "CAT_Seguimiento_Ruta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Seguimiento_Solicitud_CAT_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "CAT_Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Seguimiento_Solicitud_SeguimientoId",
                table: "Relacion_Seguimiento_Solicitud",
                column: "SeguimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Seguimiento_Solicitud_SolicitudId",
                table: "Relacion_Seguimiento_Solicitud",
                column: "SolicitudId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Seguimiento_Solicitud");

            migrationBuilder.DropTable(
                name: "CAT_Seguimiento_Ruta");
        }
    }
}
