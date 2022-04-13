using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class EstadoCiudadColonia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_Indicacion",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "CAT_Estado",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Estado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Ciudad",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoId = table.Column<byte>(type: "tinyint", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Ciudad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Ciudad_CAT_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "CAT_Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Colonia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CiudadId = table.Column<short>(type: "smallint", nullable: false),
                    Colonia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Colonia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Colonia_CAT_Ciudad_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "CAT_Ciudad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Study_AreaId",
                table: "Study",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Ciudad_EstadoId",
                table: "CAT_Ciudad",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Colonia_CiudadId",
                table: "CAT_Colonia",
                column: "CiudadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Study_CAT_Area_AreaId",
                table: "Study",
                column: "AreaId",
                principalTable: "CAT_Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Study_CAT_Area_AreaId",
                table: "Study");

            migrationBuilder.DropTable(
                name: "CAT_Colonia");

            migrationBuilder.DropTable(
                name: "CAT_Ciudad");

            migrationBuilder.DropTable(
                name: "CAT_Estado");

            migrationBuilder.DropIndex(
                name: "IX_Study_AreaId",
                table: "Study");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_Indicacion",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
