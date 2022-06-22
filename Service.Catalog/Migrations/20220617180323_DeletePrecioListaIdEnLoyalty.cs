using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class DeletePrecioListaIdEnLoyalty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Loyalty_PrecioLista");

            migrationBuilder.DropColumn(
                name: "PrecioListaStg",
                table: "CAT_Lealtad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrecioListaStg",
                table: "CAT_Lealtad",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Relacion_Loyalty_PrecioLista",
                columns: table => new
                {
                    LoyaltyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrecioListaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Loyalty_PrecioLista", x => new { x.LoyaltyId, x.PrecioListaId });
                    table.ForeignKey(
                        name: "FK_Relacion_Loyalty_PrecioLista_CAT_Lealtad_LoyaltyId",
                        column: x => x.LoyaltyId,
                        principalTable: "CAT_Lealtad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Loyalty_PrecioLista_CAT_ListaPrecio_PrecioListaId",
                        column: x => x.PrecioListaId,
                        principalTable: "CAT_ListaPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Loyalty_PrecioLista_PrecioListaId",
                table: "Relacion_Loyalty_PrecioLista",
                column: "PrecioListaId");
        }
    }
}
