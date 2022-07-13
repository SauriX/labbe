using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Report.Migrations
{
    public partial class TablaRequestModificacionesdepropiedades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "ExpedienteNombre",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "FechaFinal",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "PacienteNombre",
                table: "Request");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Request",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cargo",
                table: "Request",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Descuento",
                table: "Request",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId",
                table: "Request",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MedicoId",
                table: "Request",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioFinal",
                table: "Request",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Request",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sucursal = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreMedico = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medic", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Request_EmpresaId",
                table: "Request",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_MedicoId",
                table: "Request",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_SucursalId",
                table: "Request",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Branch_SucursalId",
                table: "Request",
                column: "SucursalId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Company_EmpresaId",
                table: "Request",
                column: "EmpresaId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Medic_MedicoId",
                table: "Request",
                column: "MedicoId",
                principalTable: "Medic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Branch_SucursalId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Company_EmpresaId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Medic_MedicoId",
                table: "Request");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Medic");

            migrationBuilder.DropIndex(
                name: "IX_Request_EmpresaId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_MedicoId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_SucursalId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Cargo",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Descuento",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "PrecioFinal",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Request");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Request",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExpedienteNombre",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFinal",
                table: "Request",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PacienteNombre",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
