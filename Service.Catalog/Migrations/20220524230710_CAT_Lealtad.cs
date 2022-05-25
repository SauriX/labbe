using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CAT_Lealtad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Lealtad_Loyality_LoyalityId",
                table: "Relacion_Promocion_Lealtad");

            migrationBuilder.DropTable(
                name: "Loyality");

            migrationBuilder.RenameColumn(
                name: "LoyalityId",
                table: "Relacion_Promocion_Lealtad",
                newName: "LoyaltyId");

            migrationBuilder.RenameIndex(
                name: "IX_Relacion_Promocion_Lealtad_LoyalityId",
                table: "Relacion_Promocion_Lealtad",
                newName: "IX_Relacion_Promocion_Lealtad_LoyaltyId");

            migrationBuilder.CreateTable(
                name: "CAT_Lealtad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoDescuento = table.Column<bool>(type: "bit", nullable: false),
                    CantidadDescuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UsuarioModId = table.Column<long>(type: "bigint", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Lealtad", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Lealtad_CAT_Lealtad_LoyaltyId",
                table: "Relacion_Promocion_Lealtad",
                column: "LoyaltyId",
                principalTable: "CAT_Lealtad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Lealtad_CAT_Lealtad_LoyaltyId",
                table: "Relacion_Promocion_Lealtad");

            migrationBuilder.DropTable(
                name: "CAT_Lealtad");

            migrationBuilder.RenameColumn(
                name: "LoyaltyId",
                table: "Relacion_Promocion_Lealtad",
                newName: "LoyalityId");

            migrationBuilder.RenameIndex(
                name: "IX_Relacion_Promocion_Lealtad_LoyaltyId",
                table: "Relacion_Promocion_Lealtad",
                newName: "IX_Relacion_Promocion_Lealtad_LoyalityId");

            migrationBuilder.CreateTable(
                name: "Loyality",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    CantidadDescuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDescuento = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loyality", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Lealtad_Loyality_LoyalityId",
                table: "Relacion_Promocion_Lealtad",
                column: "LoyalityId",
                principalTable: "Loyality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
