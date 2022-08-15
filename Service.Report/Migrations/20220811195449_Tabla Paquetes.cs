using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Report.Migrations
{
    public partial class TablaPaquetes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RequestStudy_SolicitudId",
                table: "RequestStudy");

            migrationBuilder.AddColumn<int>(
                name: "PaqueteId",
                table: "RequestStudy",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RequestPack",
                columns: table => new
                {
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaqueteId = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescuentoPorcentaje = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestPack", x => new { x.SolicitudId, x.PaqueteId });
                    table.ForeignKey(
                        name: "FK_RequestPack_Request_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Request",
                        principalColumn: "SolicitudId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestStudy_SolicitudId_PaqueteId",
                table: "RequestStudy",
                columns: new[] { "SolicitudId", "PaqueteId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStudy_RequestPack_SolicitudId_PaqueteId",
                table: "RequestStudy",
                columns: new[] { "SolicitudId", "PaqueteId" },
                principalTable: "RequestPack",
                principalColumns: new[] { "SolicitudId", "PaqueteId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestStudy_RequestPack_SolicitudId_PaqueteId",
                table: "RequestStudy");

            migrationBuilder.DropTable(
                name: "RequestPack");

            migrationBuilder.DropIndex(
                name: "IX_RequestStudy_SolicitudId_PaqueteId",
                table: "RequestStudy");

            migrationBuilder.DropColumn(
                name: "PaqueteId",
                table: "RequestStudy");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStudy_SolicitudId",
                table: "RequestStudy",
                column: "SolicitudId");
        }
    }
}
