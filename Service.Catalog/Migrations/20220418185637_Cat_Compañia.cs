using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class Cat_Compañia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Compañia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailEmpresarial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreComercial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Procedencia = table.Column<int>(type: "int", nullable: false),
                    ListaPrecioId = table.Column<int>(type: "int", nullable: false),
                    PromocionesId = table.Column<long>(type: "bigint", nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodigoPostal = table.Column<int>(type: "int", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    MunicipioId = table.Column<int>(type: "int", nullable: false),
                    RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetodoDePagoId = table.Column<int>(type: "int", nullable: false),
                    FormaDePagoId = table.Column<int>(type: "int", nullable: false),
                    LimiteDeCredito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiasCredito = table.Column<int>(type: "int", nullable: false),
                    CFDIId = table.Column<int>(type: "int", nullable: false),
                    NumeroDeCuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BancoId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<int>(type: "int", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<int>(type: "int", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Compañia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_CompañiaContacto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompañiaId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<int>(type: "int", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<int>(type: "int", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_CompañiaContacto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_CompañiaContacto_CAT_Compañia_CompañiaId",
                        column: x => x.CompañiaId,
                        principalTable: "CAT_Compañia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_CompañiaContacto_CompañiaId",
                table: "CAT_CompañiaContacto",
                column: "CompañiaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_CompañiaContacto");

            migrationBuilder.DropTable(
                name: "CAT_Compañia");
        }
    }
}
