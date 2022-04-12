using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CLinicasActualizadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Medico_Clinica",
                columns: table => new
                {
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    ClinicaId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicsIdMedico = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Medico_Clinica", x => new { x.MedicoId, x.ClinicaId });
                    table.ForeignKey(
                        name: "FK_CAT_Medico_Clinica_CAT_Clinica_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "CAT_Clinica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_Medico_Clinica_CAT_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "CAT_Medico",
                        principalColumn: "IdMedico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_Medico_Clinica_CAT_Medico_MedicsIdMedico",
                        column: x => x.MedicsIdMedico,
                        principalTable: "CAT_Medico",
                        principalColumn: "IdMedico",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Medico_Clinica_ClinicaId",
                table: "CAT_Medico_Clinica",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Medico_Clinica_MedicsIdMedico",
                table: "CAT_Medico_Clinica",
                column: "MedicsIdMedico");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Medico_Clinica");
        }
    }
}
