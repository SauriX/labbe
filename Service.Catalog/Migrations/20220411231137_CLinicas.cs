using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CLinicas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Medico_Clinica");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Medico_Clinica",
                columns: table => new
                {
                    IdMedico_Clinica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    ClinicaId = table.Column<int>(type: "int", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioCreo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Medico_Clinica", x => x.IdMedico_Clinica);
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Medico_Clinica_ClinicaId",
                table: "CAT_Medico_Clinica",
                column: "ClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Medico_Clinica_MedicoId",
                table: "CAT_Medico_Clinica",
                column: "MedicoId");
        }
    }
}
