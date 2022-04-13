using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Identity.Migrations
{
    public partial class EliminarSucursal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Sucursal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Sucursal",
                columns: table => new
                {
                    IdSucursal = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Calle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CiudadId = table.Column<long>(type: "bigint", nullable: true),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoPostal = table.Column<int>(type: "int", nullable: false),
                    ColoniaId = table.Column<long>(type: "bigint", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoId = table.Column<long>(type: "bigint", nullable: true),
                    FacturaciónId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroExterior = table.Column<int>(type: "int", nullable: false),
                    NumeroInterior = table.Column<int>(type: "int", nullable: true),
                    PresupuestosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Telefono = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Sucursal", x => x.IdSucursal);
                });
        }
    }
}
