using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CAT_Sucursal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Sucursal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColoniaId = table.Column<int>(type: "int", nullable: false),
                    NumeroExterior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroInterior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<long>(type: "bigint", nullable: true),
                    PresupuestosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacturaciónId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClinicosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaModifico = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Sucursal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAT_Sucursal_CAT_Colonia_ColoniaId",
                        column: x => x.ColoniaId,
                        principalTable: "CAT_Colonia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Sucursal_ColoniaId",
                table: "CAT_Sucursal",
                column: "ColoniaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Sucursal");
        }
    }
}
