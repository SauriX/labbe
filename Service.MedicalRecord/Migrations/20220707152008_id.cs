using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_cotizacionStudies",
                table: "cotizacionStudies");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "cotizacionStudies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_cotizacionStudies",
                table: "cotizacionStudies",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_cotizacionStudies",
                table: "cotizacionStudies");

            migrationBuilder.DropColumn(
                name: "id",
                table: "cotizacionStudies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cotizacionStudies",
                table: "cotizacionStudies",
                column: "CotizacionId");
        }
    }
}
