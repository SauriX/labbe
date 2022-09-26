using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class ClinicResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClinicResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    ParametroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreParametro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoValorId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unidades = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorInicial = table.Column<int>(type: "int", nullable: false),
                    ValorFinal = table.Column<int>(type: "int", nullable: false),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicResults_CAT_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "CAT_Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicResults_Relacion_Solicitud_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "Relacion_Solicitud_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicResults_EstudioId",
                table: "ClinicResults",
                column: "EstudioId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicResults_SolicitudId",
                table: "ClinicResults",
                column: "SolicitudId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicResults");
        }
    }
}
