using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Report.Migrations
{
    public partial class MigracionCompleta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Compañia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Convenio = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Compañia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Maquila",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Maquila", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Medico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaveMedico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreMedico = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Medico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Sucursal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sucursal = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Sucursal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Expediente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatus",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Estatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SucursalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpedienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cargo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    Parcialidad = table.Column<bool>(type: "bit", nullable: false),
                    Urgencia = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.SolicitudId);
                    table.ForeignKey(
                        name: "FK_Request_CAT_Compañia_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "CAT_Compañia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_CAT_Medico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "CAT_Medico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_CAT_Sucursal_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "CAT_Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_MedicalRecord_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "MedicalRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        name: "FK_RequestPayment_CAT_Compañia_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "CAT_Compañia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestPayment_Request_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Request",
                        principalColumn: "SolicitudId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestStudy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estudio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaqueteId = table.Column<int>(type: "int", nullable: true),
                    EstatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SucursalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaquilaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStudy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestStudy_CAT_Maquila_MaquilaId",
                        column: x => x.MaquilaId,
                        principalTable: "CAT_Maquila",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestStudy_CAT_Sucursal_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "CAT_Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestStudy_Request_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Request",
                        principalColumn: "SolicitudId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestStudy_RequestPack_SolicitudId_PaqueteId",
                        columns: x => new { x.SolicitudId, x.PaqueteId },
                        principalTable: "RequestPack",
                        principalColumns: new[] { "SolicitudId", "PaqueteId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestStudy_RequestStatus_EstatusId",
                        column: x => x.EstatusId,
                        principalTable: "RequestStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Request_EmpresaId",
                table: "Request",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_ExpedienteId",
                table: "Request",
                column: "ExpedienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_MedicoId",
                table: "Request",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_SucursalId",
                table: "Request",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestPayment_EmpresaId",
                table: "RequestPayment",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestPayment_SolicitudId",
                table: "RequestPayment",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStudy_EstatusId",
                table: "RequestStudy",
                column: "EstatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStudy_MaquilaId",
                table: "RequestStudy",
                column: "MaquilaId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStudy_SolicitudId_PaqueteId",
                table: "RequestStudy",
                columns: new[] { "SolicitudId", "PaqueteId" });

            migrationBuilder.CreateIndex(
                name: "IX_RequestStudy_SucursalId",
                table: "RequestStudy",
                column: "SucursalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestPayment");

            migrationBuilder.DropTable(
                name: "RequestStudy");

            migrationBuilder.DropTable(
                name: "CAT_Maquila");

            migrationBuilder.DropTable(
                name: "RequestPack");

            migrationBuilder.DropTable(
                name: "RequestStatus");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "CAT_Compañia");

            migrationBuilder.DropTable(
                name: "CAT_Medico");

            migrationBuilder.DropTable(
                name: "CAT_Sucursal");

            migrationBuilder.DropTable(
                name: "MedicalRecord");
        }
    }
}
