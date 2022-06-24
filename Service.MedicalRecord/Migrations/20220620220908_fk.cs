using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relacion_Expediente_Factura",
                columns: table => new
                {
                    ExpedienteID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacturaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Expediente_Factura", x => new { x.FacturaID, x.ExpedienteID });
                    table.ForeignKey(
                        name: "FK_Relacion_Expediente_Factura_CAT_Datos_Fiscales_FacturaID",
                        column: x => x.FacturaID,
                        principalTable: "CAT_Datos_Fiscales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Expediente_Factura_CAT_Expedientes_ExpedienteID",
                        column: x => x.ExpedienteID,
                        principalTable: "CAT_Expedientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Expediente_Factura_ExpedienteID",
                table: "Relacion_Expediente_Factura",
                column: "ExpedienteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Expediente_Factura");
        }
    }
}
