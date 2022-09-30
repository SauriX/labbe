using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class nuevo_Cat_Captura_ResultadosPatologicos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cat_Captura_ResultadosPatologicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    RequestStudyId = table.Column<int>(type: "int", nullable: false),
                    DescripcionMacroscopica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescripcionMicroscopica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagenPatologica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diagnostico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MuestraRecibida = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_Captura_ResultadosPatologicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cat_Captura_ResultadosPatologicos_CAT_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "CAT_Medico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cat_Captura_ResultadosPatologicos_CAT_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "CAT_Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cat_Captura_ResultadosPatologicos_Relacion_Solicitud_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "Relacion_Solicitud_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cat_Captura_ResultadosPatologicos_EstudioId",
                table: "Cat_Captura_ResultadosPatologicos",
                column: "EstudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Cat_Captura_ResultadosPatologicos_MedicoId",
                table: "Cat_Captura_ResultadosPatologicos",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cat_Captura_ResultadosPatologicos_SolicitudId",
                table: "Cat_Captura_ResultadosPatologicos",
                column: "SolicitudId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cat_Captura_ResultadosPatologicos");
        }
    }
}
