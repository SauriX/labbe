using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class AgregarTablasdeCatalogosyFkconSolicitud : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Relacion_Solicitud_Paquete",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Relacion_Solicitud_Paquete",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "DescuentoPorcentaje",
                table: "Relacion_Solicitud_Paquete",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Relacion_Solicitud_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Relacion_Solicitud_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "DescuentoPorcentaje",
                table: "Relacion_Solicitud_Estudio",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "CAT_Compañia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Compañia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Medico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clinicos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CiudadId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Sucursal", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Solicitud_CompañiaId",
                table: "CAT_Solicitud",
                column: "CompañiaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Solicitud_MedicoId",
                table: "CAT_Solicitud",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Solicitud_SucursalId",
                table: "CAT_Solicitud",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Solicitud_CAT_Compañia_CompañiaId",
                table: "CAT_Solicitud",
                column: "CompañiaId",
                principalTable: "CAT_Compañia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Solicitud_CAT_Medico_MedicoId",
                table: "CAT_Solicitud",
                column: "MedicoId",
                principalTable: "CAT_Medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Solicitud_CAT_Sucursal_SucursalId",
                table: "CAT_Solicitud",
                column: "SucursalId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Solicitud_CAT_Compañia_CompañiaId",
                table: "CAT_Solicitud");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Solicitud_CAT_Medico_MedicoId",
                table: "CAT_Solicitud");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Solicitud_CAT_Sucursal_SucursalId",
                table: "CAT_Solicitud");

            migrationBuilder.DropTable(
                name: "CAT_Compañia");

            migrationBuilder.DropTable(
                name: "CAT_Medico");

            migrationBuilder.DropTable(
                name: "CAT_Sucursal");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Solicitud_CompañiaId",
                table: "CAT_Solicitud");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Solicitud_MedicoId",
                table: "CAT_Solicitud");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Solicitud_SucursalId",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropColumn(
                name: "DescuentoPorcentaje",
                table: "Relacion_Solicitud_Paquete");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "DescuentoPorcentaje",
                table: "Relacion_Solicitud_Estudio");
        }
    }
}
