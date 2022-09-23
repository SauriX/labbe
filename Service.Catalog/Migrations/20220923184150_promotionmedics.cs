using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class promotionmedics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relacion_Promocion_Medicos",
                columns: table => new
                {
                    MedicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Promocion_Medicos", x => new { x.PromotionId, x.MedicId });
                    table.ForeignKey(
                        name: "FK_Relacion_Promocion_Medicos_CAT_Medico_MedicId",
                        column: x => x.MedicId,
                        principalTable: "CAT_Medico",
                        principalColumn: "IdMedico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Promocion_Medicos_CAT_Promocion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "CAT_Promocion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Promocion_Medicos_MedicId",
                table: "Relacion_Promocion_Medicos",
                column: "MedicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Promocion_Medicos");
        }
    }
}
