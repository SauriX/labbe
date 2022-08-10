using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Report.Migrations
{
    public partial class FKMetodoPago : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Factura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ACuenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Efectivo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TDC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Transferecia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cheque = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TDD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModifico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estatus = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestPayment_Company_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestPayment_Request_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Request",
                        principalColumn: "SolicitudId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestPayment_EmpresaId",
                table: "RequestPayment",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestPayment_SolicitudId",
                table: "RequestPayment",
                column: "SolicitudId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestPayment");
        }
    }
}
