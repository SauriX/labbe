using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class removeimagesmedics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Medico_Imagen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relacion_Medico_Imagen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MedicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ruta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Medico_Imagen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacion_Medico_Imagen_CAT_Medico_MedicId",
                        column: x => x.MedicId,
                        principalTable: "CAT_Medico",
                        principalColumn: "IdMedico",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Medico_Imagen_MedicId",
                table: "Relacion_Medico_Imagen",
                column: "MedicId");
        }
    }
}
