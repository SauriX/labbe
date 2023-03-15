using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CamposInnecesariosRutas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiasDeEntrega",
                table: "CAT_Rutas");

            migrationBuilder.DropColumn(
                name: "EstudioId",
                table: "CAT_Rutas");

            migrationBuilder.DropColumn(
                name: "FechaFinal",
                table: "CAT_Rutas");

            migrationBuilder.DropColumn(
                name: "FechaInicial",
                table: "CAT_Rutas");

            migrationBuilder.DropColumn(
                name: "FormatoDeTiempoId",
                table: "CAT_Rutas");

            migrationBuilder.DropColumn(
                name: "HoraDeEntrega",
                table: "CAT_Rutas");

            migrationBuilder.DropColumn(
                name: "HoraDeEntregaEstimada",
                table: "CAT_Rutas");

            migrationBuilder.DropColumn(
                name: "RequierePaqueteria",
                table: "CAT_Rutas");

            migrationBuilder.DropColumn(
                name: "SeguimientoPaqueteria",
                table: "CAT_Rutas");

            migrationBuilder.RenameColumn(
                name: "IdResponsableRecepcion",
                table: "CAT_Rutas",
                newName: "OrigenId");

            migrationBuilder.RenameColumn(
                name: "IdResponsableEnvio",
                table: "CAT_Rutas",
                newName: "DestinoId");

            migrationBuilder.AlterColumn<int>(
                name: "PaqueteriaId",
                table: "CAT_Rutas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraDeRecoleccion",
                table: "CAT_Rutas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "TipoTiempo",
                table: "CAT_Rutas",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoTiempo",
                table: "CAT_Rutas");

            migrationBuilder.RenameColumn(
                name: "OrigenId",
                table: "CAT_Rutas",
                newName: "IdResponsableRecepcion");

            migrationBuilder.RenameColumn(
                name: "DestinoId",
                table: "CAT_Rutas",
                newName: "IdResponsableEnvio");

            migrationBuilder.AlterColumn<int>(
                name: "PaqueteriaId",
                table: "CAT_Rutas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraDeRecoleccion",
                table: "CAT_Rutas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "DiasDeEntrega",
                table: "CAT_Rutas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EstudioId",
                table: "CAT_Rutas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFinal",
                table: "CAT_Rutas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicial",
                table: "CAT_Rutas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FormatoDeTiempoId",
                table: "CAT_Rutas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraDeEntrega",
                table: "CAT_Rutas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraDeEntregaEstimada",
                table: "CAT_Rutas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequierePaqueteria",
                table: "CAT_Rutas",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeguimientoPaqueteria",
                table: "CAT_Rutas",
                type: "int",
                nullable: true);
        }
    }
}
