using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class cotizaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Cotizaciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Procedencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombrePaciente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNac = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompaniaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Afiliacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Whatsapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaPropuesta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Estatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Cotizaciones", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Cotizaciones");
        }
    }
}
