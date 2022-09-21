using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class RelacionReactivoParametro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_CAT_Reactivo_Contpaq_ReactivoId",
                table: "CAT_Parametro");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Parametro_ReactivoId",
                table: "CAT_Parametro");

            migrationBuilder.DropColumn(
                name: "ReactivoId",
                table: "CAT_Parametro");

            migrationBuilder.CreateTable(
                name: "Relacion_Reactivo_Parametro",
                columns: table => new
                {
                    ParametroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReactivoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<int>(type: "int", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Reactivo_Parametro", x => new { x.ReactivoId, x.ParametroId });
                    table.ForeignKey(
                        name: "FK_Relacion_Reactivo_Parametro_CAT_Parametro_ParametroId",
                        column: x => x.ParametroId,
                        principalTable: "CAT_Parametro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relacion_Reactivo_Parametro_CAT_Reactivo_Contpaq_ReactivoId",
                        column: x => x.ReactivoId,
                        principalTable: "CAT_Reactivo_Contpaq",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Reactivo_Parametro_ParametroId",
                table: "Relacion_Reactivo_Parametro",
                column: "ParametroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Reactivo_Parametro");

            migrationBuilder.AddColumn<Guid>(
                name: "ReactivoId",
                table: "CAT_Parametro",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_ReactivoId",
                table: "CAT_Parametro",
                column: "ReactivoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_CAT_Reactivo_Contpaq_ReactivoId",
                table: "CAT_Parametro",
                column: "ReactivoId",
                principalTable: "CAT_Reactivo_Contpaq",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
