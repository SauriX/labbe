using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class promotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CantidadDescuento",
                table: "CAT_Promocion",
                type: "decimal(18,2)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "PrecioListaId",
                table: "CAT_Promocion",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioListaId",
                table: "CAT_Promocion");

            migrationBuilder.AlterColumn<float>(
                name: "CantidadDescuento",
                table: "CAT_Promocion",
                type: "real",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 100);
        }
    }
}
