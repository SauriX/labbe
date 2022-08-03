using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class TablasSolicitudySolicitud_Estudio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CompañiaId",
                table: "CAT_Solicitud",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "EsNuevo",
                table: "CAT_Solicitud",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Parcialidad",
                table: "CAT_Solicitud",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RutaFormato",
                table: "CAT_Solicitud",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RutaINE",
                table: "CAT_Solicitud",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RutaOrden",
                table: "CAT_Solicitud",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreo",
                table: "CAT_Solicitud",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Relacion_Solicitud_Estudio",
                columns: table => new
                {
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaqueteId = table.Column<int>(type: "int", nullable: true),
                    Paquete = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListaPrecioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListaPrecio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PromocionId = table.Column<int>(type: "int", nullable: true),
                    Promocion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    Descuento = table.Column<bool>(type: "bit", nullable: false),
                    Cargo = table.Column<bool>(type: "bit", nullable: false),
                    Copago = table.Column<bool>(type: "bit", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Solicitud_Estudio", x => new { x.SolicitudId, x.EstudioId });
                    table.ForeignKey(
                        name: "FK_Relacion_Solicitud_Estudio_CAT_Solicitud_RequestId",
                        column: x => x.RequestId,
                        principalTable: "CAT_Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Solicitud_ExpedienteId",
                table: "CAT_Solicitud",
                column: "ExpedienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Estudio_RequestId",
                table: "Relacion_Solicitud_Estudio",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Solicitud_CAT_Expedientes_ExpedienteId",
                table: "CAT_Solicitud",
                column: "ExpedienteId",
                principalTable: "CAT_Expedientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Solicitud_CAT_Expedientes_ExpedienteId",
                table: "CAT_Solicitud");

            migrationBuilder.DropTable(
                name: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Solicitud_ExpedienteId",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "EsNuevo",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "Parcialidad",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "RutaFormato",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "RutaINE",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "RutaOrden",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "UsuarioCreo",
                table: "CAT_Solicitud");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompañiaId",
                table: "CAT_Solicitud",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
