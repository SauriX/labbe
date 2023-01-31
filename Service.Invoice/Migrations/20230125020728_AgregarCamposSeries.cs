using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Billing.Migrations
{
    public partial class AgregarCamposSeries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArchivoCer",
                table: "CAT_Serie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArchivoKey",
                table: "CAT_Serie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contraseña",
                table: "CAT_Serie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmisorId",
                table: "CAT_Serie",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "CAT_Serie",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchivoCer",
                table: "CAT_Serie");

            migrationBuilder.DropColumn(
                name: "ArchivoKey",
                table: "CAT_Serie");

            migrationBuilder.DropColumn(
                name: "Contraseña",
                table: "CAT_Serie");

            migrationBuilder.DropColumn(
                name: "EmisorId",
                table: "CAT_Serie");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "CAT_Serie");
        }
    }
}
