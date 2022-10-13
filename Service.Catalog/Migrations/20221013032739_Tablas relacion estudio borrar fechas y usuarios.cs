using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class Tablasrelacionestudioborrarfechasyusuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreo",
                table: "Relacion_Estudio_WorkList");

            migrationBuilder.DropColumn(
                name: "FechaMod",
                table: "Relacion_Estudio_WorkList");

            migrationBuilder.DropColumn(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_WorkList");

            migrationBuilder.DropColumn(
                name: "UsuarioModId",
                table: "Relacion_Estudio_WorkList");

            migrationBuilder.DropColumn(
                name: "FechaCreo",
                table: "Relacion_Estudio_Reactivo");

            migrationBuilder.DropColumn(
                name: "FechaMod",
                table: "Relacion_Estudio_Reactivo");

            migrationBuilder.DropColumn(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_Reactivo");

            migrationBuilder.DropColumn(
                name: "UsuarioModId",
                table: "Relacion_Estudio_Reactivo");

            migrationBuilder.DropColumn(
                name: "FechaCreo",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.DropColumn(
                name: "FechaMod",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.DropColumn(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.DropColumn(
                name: "UsuarioModId",
                table: "Relacion_Estudio_Parametro");

            migrationBuilder.DropColumn(
                name: "FechaCreo",
                table: "Relacion_Estudio_Paquete");

            migrationBuilder.DropColumn(
                name: "FechaMod",
                table: "Relacion_Estudio_Paquete");

            migrationBuilder.DropColumn(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_Paquete");

            migrationBuilder.DropColumn(
                name: "UsuarioModId",
                table: "Relacion_Estudio_Paquete");

            migrationBuilder.DropColumn(
                name: "FechaCreo",
                table: "Relacion_Estudio_Indicacion");

            migrationBuilder.DropColumn(
                name: "FechaMod",
                table: "Relacion_Estudio_Indicacion");

            migrationBuilder.DropColumn(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_Indicacion");

            migrationBuilder.DropColumn(
                name: "UsuarioModId",
                table: "Relacion_Estudio_Indicacion");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioModificoId",
                table: "CAT_Parametro",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModifico",
                table: "CAT_Parametro",
                type: "smalldatetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreo",
                table: "Relacion_Estudio_WorkList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaMod",
                table: "Relacion_Estudio_WorkList",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_WorkList",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModId",
                table: "Relacion_Estudio_WorkList",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreo",
                table: "Relacion_Estudio_Reactivo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaMod",
                table: "Relacion_Estudio_Reactivo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_Reactivo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModId",
                table: "Relacion_Estudio_Reactivo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreo",
                table: "Relacion_Estudio_Parametro",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaMod",
                table: "Relacion_Estudio_Parametro",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_Parametro",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModId",
                table: "Relacion_Estudio_Parametro",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreo",
                table: "Relacion_Estudio_Paquete",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaMod",
                table: "Relacion_Estudio_Paquete",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_Paquete",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModId",
                table: "Relacion_Estudio_Paquete",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreo",
                table: "Relacion_Estudio_Indicacion",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaMod",
                table: "Relacion_Estudio_Indicacion",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCreoId",
                table: "Relacion_Estudio_Indicacion",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioModId",
                table: "Relacion_Estudio_Indicacion",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioModificoId",
                table: "CAT_Parametro",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModifico",
                table: "CAT_Parametro",
                type: "smalldatetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime",
                oldNullable: true);
        }
    }
}
