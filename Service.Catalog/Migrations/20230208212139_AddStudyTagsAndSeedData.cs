using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class AddStudyTagsAndSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClaveInicial",
                table: "CAT_Tipo_Tapon",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Relacion_Estudio_Etiqueta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    EtiquetaId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Estudio_Etiqueta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_Etiqueta_CAT_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_Etiqueta_CAT_Tipo_Tapon_EtiquetaId",
                        column: x => x.EtiquetaId,
                        principalTable: "CAT_Tipo_Tapon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_Etiqueta_EstudioId",
                table: "Relacion_Estudio_Etiqueta",
                column: "EstudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_Etiqueta_EtiquetaId",
                table: "Relacion_Estudio_Etiqueta",
                column: "EtiquetaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Estudio_Etiqueta");

            migrationBuilder.DropColumn(
                name: "ClaveInicial",
                table: "CAT_Tipo_Tapon");
        }
    }
}
