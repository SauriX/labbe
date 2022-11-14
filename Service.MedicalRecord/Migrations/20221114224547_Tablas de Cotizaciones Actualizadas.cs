using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class TablasdeCotizacionesActualizadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Compañia_CompañiaId",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Expedientes_ExpedienteId",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Medico_MedicoId",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Sucursal_SucursalId",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_Cotizaciones",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropColumn(
                name: "Edad",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropColumn(
                name: "Estatus",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropColumn(
                name: "FechaPropuesta",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropColumn(
                name: "Genero",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropColumn(
                name: "NombrePaciente",
                table: "CAT_Cotizaciones");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "CAT_Cotizaciones");

            migrationBuilder.RenameTable(
                name: "CAT_Cotizaciones",
                newName: "CAT_Cotizacion");

            migrationBuilder.RenameColumn(
                name: "UsuarioModId",
                table: "CAT_Cotizacion",
                newName: "UsuarioModificoId");

            migrationBuilder.RenameColumn(
                name: "FechaMod",
                table: "CAT_Cotizacion",
                newName: "FechaModifico");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Cotizaciones_SucursalId",
                table: "CAT_Cotizacion",
                newName: "IX_CAT_Cotizacion_SucursalId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Cotizaciones_MedicoId",
                table: "CAT_Cotizacion",
                newName: "IX_CAT_Cotizacion_MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Cotizaciones_ExpedienteId",
                table: "CAT_Cotizacion",
                newName: "IX_CAT_Cotizacion_ExpedienteId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Cotizaciones_CompañiaId",
                table: "CAT_Cotizacion",
                newName: "IX_CAT_Cotizacion_CompañiaId");

            migrationBuilder.AlterColumn<Guid>(
                name: "SucursalId",
                table: "CAT_Cotizacion",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "Procedencia",
                table: "CAT_Cotizacion",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)2,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Cargo",
                table: "CAT_Cotizacion",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte>(
                name: "CargoTipo",
                table: "CAT_Cotizacion",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "EstatusId",
                table: "CAT_Cotizacion",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)1);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "CAT_Cotizacion",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalEstudios",
                table: "CAT_Cotizacion",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_Cotizacion",
                table: "CAT_Cotizacion",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Estatus_Cotizacion",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estatus_Cotizacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Cotizacion_Paquete",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CotizacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaqueteId = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListaPrecioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListaPrecio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dias = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Horas = table.Column<int>(type: "int", nullable: false),
                    AplicaCargo = table.Column<bool>(type: "bit", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Cotizacion_Paquete", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacion_Cotizacion_Paquete_CAT_Cotizacion_CotizacionId",
                        column: x => x.CotizacionId,
                        principalTable: "CAT_Cotizacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Cotizacion_Estudio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CotizacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaqueteId = table.Column<int>(type: "int", nullable: true),
                    ListaPrecioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListaPrecio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dias = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Horas = table.Column<int>(type: "int", nullable: false),
                    AplicaCargo = table.Column<bool>(type: "bit", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Cotizacion_Estudio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacion_Cotizacion_Estudio_CAT_Cotizacion_CotizacionId",
                        column: x => x.CotizacionId,
                        principalTable: "CAT_Cotizacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relacion_Cotizacion_Estudio_Relacion_Cotizacion_Paquete_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Relacion_Cotizacion_Paquete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Cotizacion_EstatusId",
                table: "CAT_Cotizacion",
                column: "EstatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Cotizacion_Estudio_CotizacionId",
                table: "Relacion_Cotizacion_Estudio",
                column: "CotizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Cotizacion_Estudio_PaqueteId",
                table: "Relacion_Cotizacion_Estudio",
                column: "PaqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Cotizacion_Paquete_CotizacionId",
                table: "Relacion_Cotizacion_Paquete",
                column: "CotizacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Cotizacion_CAT_Compañia_CompañiaId",
                table: "CAT_Cotizacion",
                column: "CompañiaId",
                principalTable: "CAT_Compañia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Cotizacion_CAT_Expedientes_ExpedienteId",
                table: "CAT_Cotizacion",
                column: "ExpedienteId",
                principalTable: "CAT_Expedientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Cotizacion_CAT_Medico_MedicoId",
                table: "CAT_Cotizacion",
                column: "MedicoId",
                principalTable: "CAT_Medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Cotizacion_CAT_Sucursal_SucursalId",
                table: "CAT_Cotizacion",
                column: "SucursalId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Cotizacion_Estatus_Cotizacion_EstatusId",
                table: "CAT_Cotizacion",
                column: "EstatusId",
                principalTable: "Estatus_Cotizacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Cotizacion_CAT_Compañia_CompañiaId",
                table: "CAT_Cotizacion");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Cotizacion_CAT_Expedientes_ExpedienteId",
                table: "CAT_Cotizacion");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Cotizacion_CAT_Medico_MedicoId",
                table: "CAT_Cotizacion");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Cotizacion_CAT_Sucursal_SucursalId",
                table: "CAT_Cotizacion");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Cotizacion_Estatus_Cotizacion_EstatusId",
                table: "CAT_Cotizacion");

            migrationBuilder.DropTable(
                name: "Estatus_Cotizacion");

            migrationBuilder.DropTable(
                name: "Relacion_Cotizacion_Estudio");

            migrationBuilder.DropTable(
                name: "Relacion_Cotizacion_Paquete");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_Cotizacion",
                table: "CAT_Cotizacion");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Cotizacion_EstatusId",
                table: "CAT_Cotizacion");

            migrationBuilder.DropColumn(
                name: "CargoTipo",
                table: "CAT_Cotizacion");

            migrationBuilder.DropColumn(
                name: "EstatusId",
                table: "CAT_Cotizacion");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "CAT_Cotizacion");

            migrationBuilder.DropColumn(
                name: "TotalEstudios",
                table: "CAT_Cotizacion");

            migrationBuilder.RenameTable(
                name: "CAT_Cotizacion",
                newName: "CAT_Cotizaciones");

            migrationBuilder.RenameColumn(
                name: "UsuarioModificoId",
                table: "CAT_Cotizaciones",
                newName: "UsuarioModId");

            migrationBuilder.RenameColumn(
                name: "FechaModifico",
                table: "CAT_Cotizaciones",
                newName: "FechaMod");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Cotizacion_SucursalId",
                table: "CAT_Cotizaciones",
                newName: "IX_CAT_Cotizaciones_SucursalId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Cotizacion_MedicoId",
                table: "CAT_Cotizaciones",
                newName: "IX_CAT_Cotizaciones_MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Cotizacion_ExpedienteId",
                table: "CAT_Cotizaciones",
                newName: "IX_CAT_Cotizaciones_ExpedienteId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Cotizacion_CompañiaId",
                table: "CAT_Cotizaciones",
                newName: "IX_CAT_Cotizaciones_CompañiaId");

            migrationBuilder.AlterColumn<Guid>(
                name: "SucursalId",
                table: "CAT_Cotizaciones",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Procedencia",
                table: "CAT_Cotizaciones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValue: (byte)2);

            migrationBuilder.AlterColumn<int>(
                name: "Cargo",
                table: "CAT_Cotizaciones",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "CAT_Cotizaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Estatus",
                table: "CAT_Cotizaciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "CAT_Cotizaciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPropuesta",
                table: "CAT_Cotizaciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "CAT_Cotizaciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombrePaciente",
                table: "CAT_Cotizaciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "CAT_Cotizaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_Cotizaciones",
                table: "CAT_Cotizaciones",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Compañia_CompañiaId",
                table: "CAT_Cotizaciones",
                column: "CompañiaId",
                principalTable: "CAT_Compañia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Expedientes_ExpedienteId",
                table: "CAT_Cotizaciones",
                column: "ExpedienteId",
                principalTable: "CAT_Expedientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Medico_MedicoId",
                table: "CAT_Cotizaciones",
                column: "MedicoId",
                principalTable: "CAT_Medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Cotizaciones_CAT_Sucursal_SucursalId",
                table: "CAT_Cotizaciones",
                column: "SucursalId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
