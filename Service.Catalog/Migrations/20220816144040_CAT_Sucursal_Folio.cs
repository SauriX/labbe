using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CAT_Sucursal_Folio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_ciudadBranch");

            migrationBuilder.CreateTable(
                name: "CAT_Sucursal_Folio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoId = table.Column<byte>(type: "tinyint", nullable: false),
                    CiudadId = table.Column<short>(type: "smallint", nullable: false),
                    ConsecutivoEstado = table.Column<byte>(type: "tinyint", nullable: false),
                    ConsecutivoCiudad = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Sucursal_Folio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Sucursal_Folio_CAT_Ciudad_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "CAT_Ciudad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_Sucursal_Folio_CAT_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "CAT_Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Sucursal_Folio_CiudadId",
                table: "CAT_Sucursal_Folio",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Sucursal_Folio_EstadoId",
                table: "CAT_Sucursal_Folio",
                column: "EstadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Sucursal_Folio");

            migrationBuilder.CreateTable(
                name: "CAT_ciudadBranch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ciudadBranch", x => x.Id);
                });
        }
    }
}
