using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class routetracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

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



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cat_PendientesDeEnviar");




        }
    }
}
