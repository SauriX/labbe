using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class EliminarColumnasNRutas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaqueteriaId",
                table: "CAT_Rutas");

            migrationBuilder.DropColumn(
                name: "SucursalDestinoId",
                table: "CAT_Rutas");

            migrationBuilder.DropColumn(
                name: "SucursalOrigenId",
                table: "CAT_Rutas");

            migrationBuilder.AlterColumn<Guid>(
                name: "PrecioListaId",
                table: "CAT_Lealtad",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaqueteriaId",
                table: "CAT_Rutas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SucursalDestinoId",
                table: "CAT_Rutas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SucursalOrigenId",
                table: "CAT_Rutas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PrecioListaId",
                table: "CAT_Lealtad",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
