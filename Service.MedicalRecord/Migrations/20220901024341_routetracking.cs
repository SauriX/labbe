using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class routetracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RutaFormato",
                table: "CAT_Solicitud",
                newName: "RutaINEReverso");

            migrationBuilder.CreateTable(
                name: "Cat_PendientesDeEnviar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SegumientoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RutaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SucursalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaDeEntregaEstimada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoraDeRecoleccion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_PendientesDeEnviar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cat_PendientesDeEnviar_CAT_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "CAT_Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Seguimiento_Ruta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SucursalDestinoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SucursalOrigenId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MuestraId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolicitudId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EscaneoCodigoBarras = table.Column<bool>(type: "bit", nullable: false),
                    Temperatura = table.Column<double>(type: "float", nullable: false),
                    ClaveEstudio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estudio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PacienteId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Escaneado = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Seguimiento_Ruta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Solicitud_Imagen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ruta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Solicitud_Imagen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacion_Solicitud_Imagen_CAT_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "CAT_Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cat_PendientesDeEnviar_SolicitudId",
                table: "Cat_PendientesDeEnviar",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Imagen_SolicitudId",
                table: "Relacion_Solicitud_Imagen",
                column: "SolicitudId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cat_PendientesDeEnviar");

            migrationBuilder.DropTable(
                name: "CAT_Seguimiento_Ruta");

            migrationBuilder.DropTable(
                name: "Relacion_Solicitud_Imagen");

            migrationBuilder.RenameColumn(
                name: "RutaINEReverso",
                table: "CAT_Solicitud",
                newName: "RutaFormato");
        }
    }
}
