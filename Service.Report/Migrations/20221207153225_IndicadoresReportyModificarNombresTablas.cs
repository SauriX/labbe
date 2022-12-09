using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Report.Migrations
{
    public partial class IndicadoresReportyModificarNombresTablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_CAT_Compañia_EmpresaId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_CAT_Medico_MedicoId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_CAT_Sucursal_SucursalId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_MedicalRecord_ExpedienteId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestPack_Request_SolicitudId",
                table: "RequestPack");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestPayment_CAT_Compañia_EmpresaId",
                table: "RequestPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestPayment_Request_SolicitudId",
                table: "RequestPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestStudy_CAT_Maquila_MaquilaId",
                table: "RequestStudy");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestStudy_CAT_Sucursal_SucursalId",
                table: "RequestStudy");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestStudy_Request_SolicitudId",
                table: "RequestStudy");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestStudy_RequestPack_SolicitudId_PaqueteId",
                table: "RequestStudy");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestStudy_RequestStatus_EstatusId",
                table: "RequestStudy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestStudy",
                table: "RequestStudy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestPayment",
                table: "RequestPayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Request",
                table: "Request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalRecord",
                table: "MedicalRecord");

            migrationBuilder.RenameTable(
                name: "RequestStudy",
                newName: "Relación_Solicitud_Estudio");

            migrationBuilder.RenameTable(
                name: "RequestPayment",
                newName: "CAT_Corte_Caja");

            migrationBuilder.RenameTable(
                name: "Request",
                newName: "CAT_Solicitud");

            migrationBuilder.RenameTable(
                name: "MedicalRecord",
                newName: "CAT_Expedientes");

            migrationBuilder.RenameIndex(
                name: "IX_RequestStudy_SucursalId",
                table: "Relación_Solicitud_Estudio",
                newName: "IX_Relación_Solicitud_Estudio_SucursalId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestStudy_SolicitudId_PaqueteId",
                table: "Relación_Solicitud_Estudio",
                newName: "IX_Relación_Solicitud_Estudio_SolicitudId_PaqueteId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestStudy_MaquilaId",
                table: "Relación_Solicitud_Estudio",
                newName: "IX_Relación_Solicitud_Estudio_MaquilaId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestStudy_EstatusId",
                table: "Relación_Solicitud_Estudio",
                newName: "IX_Relación_Solicitud_Estudio_EstatusId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestPayment_SolicitudId",
                table: "CAT_Corte_Caja",
                newName: "IX_CAT_Corte_Caja_SolicitudId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestPayment_EmpresaId",
                table: "CAT_Corte_Caja",
                newName: "IX_CAT_Corte_Caja_EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_SucursalId",
                table: "CAT_Solicitud",
                newName: "IX_CAT_Solicitud_SucursalId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_MedicoId",
                table: "CAT_Solicitud",
                newName: "IX_CAT_Solicitud_MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_ExpedienteId",
                table: "CAT_Solicitud",
                newName: "IX_CAT_Solicitud_ExpedienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_EmpresaId",
                table: "CAT_Solicitud",
                newName: "IX_CAT_Solicitud_EmpresaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relación_Solicitud_Estudio",
                table: "Relación_Solicitud_Estudio",
                columns: new[] { "SolicitudId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_Corte_Caja",
                table: "CAT_Corte_Caja",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_Solicitud",
                table: "CAT_Solicitud",
                column: "SolicitudId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_Expedientes",
                table: "CAT_Expedientes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CAT_Indicadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pacientes = table.Column<int>(type: "int", nullable: false),
                    Ingresos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostoReactivo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostoTomaCalculado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UtilidadOperacion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SucursalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostoFijo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Indicadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Indicadores_CAT_Sucursal_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "CAT_Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Indicadores_SucursalId",
                table: "CAT_Indicadores",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Corte_Caja_CAT_Compañia_EmpresaId",
                table: "CAT_Corte_Caja",
                column: "EmpresaId",
                principalTable: "CAT_Compañia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Corte_Caja_CAT_Solicitud_SolicitudId",
                table: "CAT_Corte_Caja",
                column: "SolicitudId",
                principalTable: "CAT_Solicitud",
                principalColumn: "SolicitudId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Solicitud_CAT_Compañia_EmpresaId",
                table: "CAT_Solicitud",
                column: "EmpresaId",
                principalTable: "CAT_Compañia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Solicitud_CAT_Expedientes_ExpedienteId",
                table: "CAT_Solicitud",
                column: "ExpedienteId",
                principalTable: "CAT_Expedientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Solicitud_CAT_Medico_MedicoId",
                table: "CAT_Solicitud",
                column: "MedicoId",
                principalTable: "CAT_Medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Solicitud_CAT_Sucursal_SucursalId",
                table: "CAT_Solicitud",
                column: "SucursalId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relación_Solicitud_Estudio_CAT_Maquila_MaquilaId",
                table: "Relación_Solicitud_Estudio",
                column: "MaquilaId",
                principalTable: "CAT_Maquila",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relación_Solicitud_Estudio_CAT_Solicitud_SolicitudId",
                table: "Relación_Solicitud_Estudio",
                column: "SolicitudId",
                principalTable: "CAT_Solicitud",
                principalColumn: "SolicitudId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relación_Solicitud_Estudio_CAT_Sucursal_SucursalId",
                table: "Relación_Solicitud_Estudio",
                column: "SucursalId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relación_Solicitud_Estudio_RequestPack_SolicitudId_PaqueteId",
                table: "Relación_Solicitud_Estudio",
                columns: new[] { "SolicitudId", "PaqueteId" },
                principalTable: "RequestPack",
                principalColumns: new[] { "SolicitudId", "PaqueteId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relación_Solicitud_Estudio_RequestStatus_EstatusId",
                table: "Relación_Solicitud_Estudio",
                column: "EstatusId",
                principalTable: "RequestStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestPack_CAT_Solicitud_SolicitudId",
                table: "RequestPack",
                column: "SolicitudId",
                principalTable: "CAT_Solicitud",
                principalColumn: "SolicitudId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Corte_Caja_CAT_Compañia_EmpresaId",
                table: "CAT_Corte_Caja");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Corte_Caja_CAT_Solicitud_SolicitudId",
                table: "CAT_Corte_Caja");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Solicitud_CAT_Compañia_EmpresaId",
                table: "CAT_Solicitud");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Solicitud_CAT_Expedientes_ExpedienteId",
                table: "CAT_Solicitud");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Solicitud_CAT_Medico_MedicoId",
                table: "CAT_Solicitud");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Solicitud_CAT_Sucursal_SucursalId",
                table: "CAT_Solicitud");

            migrationBuilder.DropForeignKey(
                name: "FK_Relación_Solicitud_Estudio_CAT_Maquila_MaquilaId",
                table: "Relación_Solicitud_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_Relación_Solicitud_Estudio_CAT_Solicitud_SolicitudId",
                table: "Relación_Solicitud_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_Relación_Solicitud_Estudio_CAT_Sucursal_SucursalId",
                table: "Relación_Solicitud_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_Relación_Solicitud_Estudio_RequestPack_SolicitudId_PaqueteId",
                table: "Relación_Solicitud_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_Relación_Solicitud_Estudio_RequestStatus_EstatusId",
                table: "Relación_Solicitud_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestPack_CAT_Solicitud_SolicitudId",
                table: "RequestPack");

            migrationBuilder.DropTable(
                name: "CAT_Indicadores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Relación_Solicitud_Estudio",
                table: "Relación_Solicitud_Estudio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_Solicitud",
                table: "CAT_Solicitud");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_Expedientes",
                table: "CAT_Expedientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_Corte_Caja",
                table: "CAT_Corte_Caja");

            migrationBuilder.RenameTable(
                name: "Relación_Solicitud_Estudio",
                newName: "RequestStudy");

            migrationBuilder.RenameTable(
                name: "CAT_Solicitud",
                newName: "Request");

            migrationBuilder.RenameTable(
                name: "CAT_Expedientes",
                newName: "MedicalRecord");

            migrationBuilder.RenameTable(
                name: "CAT_Corte_Caja",
                newName: "RequestPayment");

            migrationBuilder.RenameIndex(
                name: "IX_Relación_Solicitud_Estudio_SucursalId",
                table: "RequestStudy",
                newName: "IX_RequestStudy_SucursalId");

            migrationBuilder.RenameIndex(
                name: "IX_Relación_Solicitud_Estudio_SolicitudId_PaqueteId",
                table: "RequestStudy",
                newName: "IX_RequestStudy_SolicitudId_PaqueteId");

            migrationBuilder.RenameIndex(
                name: "IX_Relación_Solicitud_Estudio_MaquilaId",
                table: "RequestStudy",
                newName: "IX_RequestStudy_MaquilaId");

            migrationBuilder.RenameIndex(
                name: "IX_Relación_Solicitud_Estudio_EstatusId",
                table: "RequestStudy",
                newName: "IX_RequestStudy_EstatusId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Solicitud_SucursalId",
                table: "Request",
                newName: "IX_Request_SucursalId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Solicitud_MedicoId",
                table: "Request",
                newName: "IX_Request_MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Solicitud_ExpedienteId",
                table: "Request",
                newName: "IX_Request_ExpedienteId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Solicitud_EmpresaId",
                table: "Request",
                newName: "IX_Request_EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Corte_Caja_SolicitudId",
                table: "RequestPayment",
                newName: "IX_RequestPayment_SolicitudId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Corte_Caja_EmpresaId",
                table: "RequestPayment",
                newName: "IX_RequestPayment_EmpresaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestStudy",
                table: "RequestStudy",
                columns: new[] { "SolicitudId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Request",
                table: "Request",
                column: "SolicitudId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalRecord",
                table: "MedicalRecord",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestPayment",
                table: "RequestPayment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_CAT_Compañia_EmpresaId",
                table: "Request",
                column: "EmpresaId",
                principalTable: "CAT_Compañia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_CAT_Medico_MedicoId",
                table: "Request",
                column: "MedicoId",
                principalTable: "CAT_Medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_CAT_Sucursal_SucursalId",
                table: "Request",
                column: "SucursalId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_MedicalRecord_ExpedienteId",
                table: "Request",
                column: "ExpedienteId",
                principalTable: "MedicalRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestPack_Request_SolicitudId",
                table: "RequestPack",
                column: "SolicitudId",
                principalTable: "Request",
                principalColumn: "SolicitudId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestPayment_CAT_Compañia_EmpresaId",
                table: "RequestPayment",
                column: "EmpresaId",
                principalTable: "CAT_Compañia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestPayment_Request_SolicitudId",
                table: "RequestPayment",
                column: "SolicitudId",
                principalTable: "Request",
                principalColumn: "SolicitudId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStudy_CAT_Maquila_MaquilaId",
                table: "RequestStudy",
                column: "MaquilaId",
                principalTable: "CAT_Maquila",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStudy_CAT_Sucursal_SucursalId",
                table: "RequestStudy",
                column: "SucursalId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStudy_Request_SolicitudId",
                table: "RequestStudy",
                column: "SolicitudId",
                principalTable: "Request",
                principalColumn: "SolicitudId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStudy_RequestPack_SolicitudId_PaqueteId",
                table: "RequestStudy",
                columns: new[] { "SolicitudId", "PaqueteId" },
                principalTable: "RequestPack",
                principalColumns: new[] { "SolicitudId", "PaqueteId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStudy_RequestStatus_EstatusId",
                table: "RequestStudy",
                column: "EstatusId",
                principalTable: "RequestStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
