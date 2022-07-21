using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Report.Migrations
{
    public partial class CrearTablaRequestStudy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Request",
                newName: "EstatusId");

            migrationBuilder.AddColumn<string>(
                name: "Clave",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SolicitudId",
                table: "Request",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RequestStudy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estudio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    Descuento = table.Column<bool>(type: "bit", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStudy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestStudy_Request_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestStudy_SolicitudId",
                table: "RequestStudy",
                column: "SolicitudId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestStudy");

            migrationBuilder.DropColumn(
                name: "Clave",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "SolicitudId",
                table: "Request");

            migrationBuilder.RenameColumn(
                name: "EstatusId",
                table: "Request",
                newName: "Status");
        }
    }
}
