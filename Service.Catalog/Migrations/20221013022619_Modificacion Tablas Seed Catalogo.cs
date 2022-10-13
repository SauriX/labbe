using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class ModificacionTablasSeedCatalogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_Area_AreaId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_Maquilador_MaquiladorId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_Metodo_MetodoId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_Tipo_Tapon_TaponId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_TipoMuestra_SampleTypeId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_CAT_Area_AreaId",
                table: "CAT_Parametro");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_CAT_Units_UnidadSiId",
                table: "CAT_Parametro");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "CAT_Units");

            migrationBuilder.DropColumn(
                name: "FechaMod",
                table: "CAT_Estudio");

            migrationBuilder.DropColumn(
                name: "UsuarioModId",
                table: "CAT_Estudio");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorInicial",
                table: "CAT_Parametro",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioCreoId",
                table: "CAT_Parametro",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "UnidadSiId",
                table: "CAT_Parametro",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UnidadId",
                table: "CAT_Parametro",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TipoValor",
                table: "CAT_Parametro",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "0");

            migrationBuilder.AlterColumn<string>(
                name: "NombreCorto",
                table: "CAT_Parametro",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModifico",
                table: "CAT_Parametro",
                type: "smalldatetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreo",
                table: "CAT_Parametro",
                type: "smalldatetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "CAT_Parametro",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "CAT_Parametro",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NombreLargo",
                table: "CAT_Paquete",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "CAT_Paquete",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModifico",
                table: "CAT_Paquete",
                type: "smalldatetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreo",
                table: "CAT_Paquete",
                type: "smalldatetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioCreoId",
                table: "CAT_Maquilador",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroExterior",
                table: "CAT_Maquilador",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreo",
                table: "CAT_Maquilador",
                type: "smalldatetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");

            migrationBuilder.AlterColumn<string>(
                name: "Calle",
                table: "CAT_Maquilador",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioCreoId",
                table: "CAT_Estudio",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "TaponId",
                table: "CAT_Estudio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SampleTypeId",
                table: "CAT_Estudio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NombreCorto",
                table: "CAT_Estudio",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "MetodoId",
                table: "CAT_Estudio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MaquiladorId",
                table: "CAT_Estudio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreo",
                table: "CAT_Estudio",
                type: "smalldatetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "CAT_Estudio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "CAT_Estudio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModifico",
                table: "CAT_Estudio",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioModificoId",
                table: "CAT_Estudio",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_Area_AreaId",
                table: "CAT_Estudio",
                column: "AreaId",
                principalTable: "CAT_Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_Maquilador_MaquiladorId",
                table: "CAT_Estudio",
                column: "MaquiladorId",
                principalTable: "CAT_Maquilador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_Metodo_MetodoId",
                table: "CAT_Estudio",
                column: "MetodoId",
                principalTable: "CAT_Metodo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_Tipo_Tapon_TaponId",
                table: "CAT_Estudio",
                column: "TaponId",
                principalTable: "CAT_Tipo_Tapon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_TipoMuestra_SampleTypeId",
                table: "CAT_Estudio",
                column: "SampleTypeId",
                principalTable: "CAT_TipoMuestra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_CAT_Area_AreaId",
                table: "CAT_Parametro",
                column: "AreaId",
                principalTable: "CAT_Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_CAT_Units_UnidadSiId",
                table: "CAT_Parametro",
                column: "UnidadSiId",
                principalTable: "CAT_Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_Area_AreaId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_Maquilador_MaquiladorId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_Metodo_MetodoId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_Tipo_Tapon_TaponId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_TipoMuestra_SampleTypeId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_CAT_Area_AreaId",
                table: "CAT_Parametro");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_CAT_Units_UnidadSiId",
                table: "CAT_Parametro");

            migrationBuilder.DropColumn(
                name: "FechaModifico",
                table: "CAT_Estudio");

            migrationBuilder.DropColumn(
                name: "UsuarioModificoId",
                table: "CAT_Estudio");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "CAT_Units",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorInicial",
                table: "CAT_Parametro",
                type: "decimal(18,2)",
                maxLength: 100,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioCreoId",
                table: "CAT_Parametro",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnidadSiId",
                table: "CAT_Parametro",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnidadId",
                table: "CAT_Parametro",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TipoValor",
                table: "CAT_Parametro",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "0",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NombreCorto",
                table: "CAT_Parametro",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModifico",
                table: "CAT_Parametro",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreo",
                table: "CAT_Parametro",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "CAT_Parametro",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "CAT_Parametro",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NombreLargo",
                table: "CAT_Paquete",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "CAT_Paquete",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModifico",
                table: "CAT_Paquete",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreo",
                table: "CAT_Paquete",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioCreoId",
                table: "CAT_Maquilador",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroExterior",
                table: "CAT_Maquilador",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreo",
                table: "CAT_Maquilador",
                type: "smalldatetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Calle",
                table: "CAT_Maquilador",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioCreoId",
                table: "CAT_Estudio",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TaponId",
                table: "CAT_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SampleTypeId",
                table: "CAT_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NombreCorto",
                table: "CAT_Estudio",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MetodoId",
                table: "CAT_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaquiladorId",
                table: "CAT_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreo",
                table: "CAT_Estudio",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "CAT_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "CAT_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaMod",
                table: "CAT_Estudio",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioModId",
                table: "CAT_Estudio",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_Area_AreaId",
                table: "CAT_Estudio",
                column: "AreaId",
                principalTable: "CAT_Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_Maquilador_MaquiladorId",
                table: "CAT_Estudio",
                column: "MaquiladorId",
                principalTable: "CAT_Maquilador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_Metodo_MetodoId",
                table: "CAT_Estudio",
                column: "MetodoId",
                principalTable: "CAT_Metodo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_Tipo_Tapon_TaponId",
                table: "CAT_Estudio",
                column: "TaponId",
                principalTable: "CAT_Tipo_Tapon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_TipoMuestra_SampleTypeId",
                table: "CAT_Estudio",
                column: "SampleTypeId",
                principalTable: "CAT_TipoMuestra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_CAT_Area_AreaId",
                table: "CAT_Parametro",
                column: "AreaId",
                principalTable: "CAT_Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_CAT_Units_UnidadSiId",
                table: "CAT_Parametro",
                column: "UnidadSiId",
                principalTable: "CAT_Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
