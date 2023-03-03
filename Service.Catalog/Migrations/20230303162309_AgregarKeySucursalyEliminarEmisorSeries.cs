using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class AgregarKeySucursalyEliminarEmisorSeries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmisorId",
                table: "CAT_Serie");

            migrationBuilder.AddColumn<string>(
                name: "SucursalKey",
                table: "CAT_Sucursal",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SucursalKey",
                table: "CAT_Sucursal");

            migrationBuilder.AddColumn<Guid>(
                name: "EmisorId",
                table: "CAT_Serie",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
