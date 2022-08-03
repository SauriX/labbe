using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class TablaSolicitud_Paquete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_CAT_Solicitud_RequestId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Solicitud_Estudio_RequestId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "Paquete",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.AlterColumn<Guid>(
                name: "MedicoId",
                table: "CAT_Solicitud",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "Relacion_Solicitud_Paquete",
                columns: table => new
                {
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaqueteId = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListaPrecioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListaPrecio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PromocionId = table.Column<int>(type: "int", nullable: true),
                    Promocion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descuento = table.Column<bool>(type: "bit", nullable: false),
                    Cargo = table.Column<bool>(type: "bit", nullable: false),
                    Copago = table.Column<bool>(type: "bit", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Solicitud_Paquete", x => new { x.SolicitudId, x.PaqueteId });
                    table.ForeignKey(
                        name: "FK_Relacion_Solicitud_Paquete_CAT_Solicitud_RequestId",
                        column: x => x.RequestId,
                        principalTable: "CAT_Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Estudio_SolicitudId_PaqueteId",
                table: "Relacion_Solicitud_Estudio",
                columns: new[] { "SolicitudId", "PaqueteId" });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Paquete_RequestId",
                table: "Relacion_Solicitud_Paquete",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_CAT_Solicitud_SolicitudId",
                table: "Relacion_Solicitud_Estudio",
                column: "SolicitudId",
                principalTable: "CAT_Solicitud",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_Relacion_Solicitud_Paquete_SolicitudId_PaqueteId",
                table: "Relacion_Solicitud_Estudio",
                columns: new[] { "SolicitudId", "PaqueteId" },
                principalTable: "Relacion_Solicitud_Paquete",
                principalColumns: new[] { "SolicitudId", "PaqueteId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_CAT_Solicitud_SolicitudId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_Relacion_Solicitud_Paquete_SolicitudId_PaqueteId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropTable(
                name: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Solicitud_Estudio_SolicitudId_PaqueteId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.AddColumn<string>(
                name: "Paquete",
                table: "Relacion_Solicitud_Estudio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RequestId",
                table: "Relacion_Solicitud_Estudio",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MedicoId",
                table: "CAT_Solicitud",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Estudio_RequestId",
                table: "Relacion_Solicitud_Estudio",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_CAT_Solicitud_RequestId",
                table: "Relacion_Solicitud_Estudio",
                column: "RequestId",
                principalTable: "CAT_Solicitud",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
