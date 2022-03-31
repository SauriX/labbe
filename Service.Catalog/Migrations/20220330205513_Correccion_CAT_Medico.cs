using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class Correccion_CAT_Medico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Medicos_Clinica");

            migrationBuilder.AlterColumn<long>(
                name: "Telefono",
                table: "CAT_Medico",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Celular",
                table: "CAT_Medico",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "CAT_Medico_Clinica",
                columns: table => new
                {
                    IdMedico_Clinica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicoId = table.Column<long>(type: "bigint", nullable: false),
                    MedicoIdMedico = table.Column<int>(type: "int", nullable: true),
                    ClinicaId = table.Column<long>(type: "bigint", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioCreo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Medico_Clinica", x => x.IdMedico_Clinica);
                    table.ForeignKey(
                        name: "FK_CAT_Medico_Clinica_CAT_Medico_MedicoIdMedico",
                        column: x => x.MedicoIdMedico,
                        principalTable: "CAT_Medico",
                        principalColumn: "IdMedico",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Medico_Clinica_MedicoIdMedico",
                table: "CAT_Medico_Clinica",
                column: "MedicoIdMedico");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Medico_Clinica");

            migrationBuilder.AlterColumn<int>(
                name: "Telefono",
                table: "CAT_Medico",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Celular",
                table: "CAT_Medico",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "CAT_Medicos_Clinica",
                columns: table => new
                {
                    IdMedico_Clinica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    ClinicaId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicoId = table.Column<long>(type: "bigint", nullable: false),
                    MedicoIdMedico = table.Column<int>(type: "int", nullable: true),
                    UsuarioCreo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Medicos_Clinica", x => x.IdMedico_Clinica);
                    table.ForeignKey(
                        name: "FK_CAT_Medicos_Clinica_CAT_Medico_MedicoIdMedico",
                        column: x => x.MedicoIdMedico,
                        principalTable: "CAT_Medico",
                        principalColumn: "IdMedico",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Medicos_Clinica_MedicoIdMedico",
                table: "CAT_Medicos_Clinica",
                column: "MedicoIdMedico");
        }
    }
}
