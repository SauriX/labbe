using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class Tablas_Etiquetas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orden",
                table: "Relacion_Solicitud_Etiquetas");

            migrationBuilder.RenameColumn(
                name: "Tapon",
                table: "Relacion_Solicitud_Etiquetas",
                newName: "NombreEtiqueta");

            migrationBuilder.RenameColumn(
                name: "Suero",
                table: "Relacion_Solicitud_Etiquetas",
                newName: "EtiquetaId");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Relacion_Solicitud_Etiquetas",
                newName: "DestinoId");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Relacion_Solicitud_Etiquetas",
                newName: "FechaCreo");

            migrationBuilder.RenameColumn(
                name: "Clave",
                table: "Relacion_Solicitud_Etiquetas",
                newName: "Destino");

            migrationBuilder.AddColumn<string>(
                name: "ClaveEtiqueta",
                table: "Relacion_Solicitud_Etiquetas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClaveInicial",
                table: "Relacion_Solicitud_Etiquetas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Relacion_Solicitud_Etiquetas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "DestinoTipo",
                table: "Relacion_Solicitud_Etiquetas",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModifico",
                table: "Relacion_Solicitud_Etiquetas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCreoId",
                table: "Relacion_Solicitud_Etiquetas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioModificoId",
                table: "Relacion_Solicitud_Etiquetas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RequestTagStudy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudEtiquetaId = table.Column<int>(type: "int", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    NombreEstudio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTagStudy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestTagStudy_Relacion_Solicitud_Etiquetas_SolicitudEtiquetaId",
                        column: x => x.SolicitudEtiquetaId,
                        principalTable: "Relacion_Solicitud_Etiquetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestTagStudy_SolicitudEtiquetaId",
                table: "RequestTagStudy",
                column: "SolicitudEtiquetaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestTagStudy");

            migrationBuilder.DropColumn(
                name: "ClaveEtiqueta",
                table: "Relacion_Solicitud_Etiquetas");

            migrationBuilder.DropColumn(
                name: "ClaveInicial",
                table: "Relacion_Solicitud_Etiquetas");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Relacion_Solicitud_Etiquetas");

            migrationBuilder.DropColumn(
                name: "DestinoTipo",
                table: "Relacion_Solicitud_Etiquetas");

            migrationBuilder.DropColumn(
                name: "FechaModifico",
                table: "Relacion_Solicitud_Etiquetas");

            migrationBuilder.DropColumn(
                name: "UsuarioCreoId",
                table: "Relacion_Solicitud_Etiquetas");

            migrationBuilder.DropColumn(
                name: "UsuarioModificoId",
                table: "Relacion_Solicitud_Etiquetas");

            migrationBuilder.RenameColumn(
                name: "NombreEtiqueta",
                table: "Relacion_Solicitud_Etiquetas",
                newName: "Tapon");

            migrationBuilder.RenameColumn(
                name: "FechaCreo",
                table: "Relacion_Solicitud_Etiquetas",
                newName: "Fecha");

            migrationBuilder.RenameColumn(
                name: "EtiquetaId",
                table: "Relacion_Solicitud_Etiquetas",
                newName: "Suero");

            migrationBuilder.RenameColumn(
                name: "DestinoId",
                table: "Relacion_Solicitud_Etiquetas",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Destino",
                table: "Relacion_Solicitud_Etiquetas",
                newName: "Clave");

            migrationBuilder.AddColumn<int>(
                name: "Orden",
                table: "Relacion_Solicitud_Etiquetas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
