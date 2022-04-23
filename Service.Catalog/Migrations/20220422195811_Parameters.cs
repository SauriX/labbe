using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class Parameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cat_Formato",
                columns: table => new
                {
                    NombreFormato = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_Formato", x => x.NombreFormato);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Parametro",
                columns: table => new
                {
                    IdParametro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ValorInicial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombreCorto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Unidades = table.Column<double>(type: "float", nullable: false),
                    Formula = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Formato = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    NombreFormato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReagentId = table.Column<int>(type: "int", nullable: false),
                    UnidadSi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FCSI = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Parametro", x => x.IdParametro);
                    table.ForeignKey(
                        name: "FK_CAT_Parametro_CAT_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "CAT_Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_Parametro_CAT_Reactivo_Contpaq_ReagentId",
                        column: x => x.ReagentId,
                        principalTable: "CAT_Reactivo_Contpaq",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAT_Tipo_Valor",
                columns: table => new
                {
                    IdTipo_Valor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdParametro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parametresIdParametro = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorInicialNumerico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorFinalNumerico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RangoEdadInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RangoEdadFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HombreValorInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HombreValorFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MujerValorInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MujerValorFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedidaTiempo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescripcionTexto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescripcionParrafo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Tipo_Valor", x => x.IdTipo_Valor);
                    table.ForeignKey(
                        name: "FK_CAT_Tipo_Valor_CAT_Parametro_parametresIdParametro",
                        column: x => x.parametresIdParametro,
                        principalTable: "CAT_Parametro",
                        principalColumn: "IdParametro",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_AreaId",
                table: "CAT_Parametro",
                column: "AreaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_ReagentId",
                table: "CAT_Parametro",
                column: "ReagentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Tipo_Valor_parametresIdParametro",
                table: "CAT_Tipo_Valor",
                column: "parametresIdParametro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cat_Formato");

            migrationBuilder.DropTable(
                name: "CAT_Tipo_Valor");

            migrationBuilder.DropTable(
                name: "CAT_Parametro");
        }
    }
}
