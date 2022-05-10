using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class studies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoMuestraId",
                table: "CAT_Estudio",
                newName: "TiempoResultado");

            migrationBuilder.RenameColumn(
                name: "TiempoRespuesta",
                table: "CAT_Estudio",
                newName: "TaponId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioModId",
                table: "CAT_Estudio",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioCreoId",
                table: "CAT_Estudio",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "CAT_Estudio",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Prioridad",
                table: "CAT_Estudio",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NombreCorto",
                table: "CAT_Estudio",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "CAT_Estudio",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaMod",
                table: "CAT_Estudio",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreo",
                table: "CAT_Estudio",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Clave",
                table: "CAT_Estudio",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "CAT_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dias",
                table: "CAT_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SampleTypeId",
                table: "CAT_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Urgencia",
                table: "CAT_Estudio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CAT_Tipo_Tapon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Tipo_Tapon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Estudio_Reactivo",
                columns: table => new
                {
                    ReagentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<int>(type: "int", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Estudio_Reactivo", x => new { x.EstudioId, x.ReagentId });
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_Reactivo_CAT_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_Reactivo_CAT_Reactivo_Contpaq_ReagentId",
                        column: x => x.ReagentId,
                        principalTable: "CAT_Reactivo_Contpaq",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Estudio_WorkList",
                columns: table => new
                {
                    WorkListId = table.Column<int>(type: "int", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<int>(type: "int", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Estudio_WorkList", x => new { x.EstudioId, x.WorkListId });
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_WorkList_CAT_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_WorkList_CAT_ListaTrabajo_WorkListId",
                        column: x => x.WorkListId,
                        principalTable: "CAT_ListaTrabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_Estudio_Paquete",
                columns: table => new
                {
                    PacketId = table.Column<int>(type: "int", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<int>(type: "int", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Estudio_Paquete", x => new { x.EstudioId, x.PacketId });
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_Paquete_CAT_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_Paquete_Packet_PacketId",
                        column: x => x.PacketId,
                        principalTable: "Packet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Estudio_FormatoId",
                table: "CAT_Estudio",
                column: "FormatoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Estudio_MaquiladorId",
                table: "CAT_Estudio",
                column: "MaquiladorId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Estudio_MetodoId",
                table: "CAT_Estudio",
                column: "MetodoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Estudio_SampleTypeId",
                table: "CAT_Estudio",
                column: "SampleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Estudio_TaponId",
                table: "CAT_Estudio",
                column: "TaponId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_Paquete_PacketId",
                table: "Relacion_Estudio_Paquete",
                column: "PacketId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_Reactivo_ReagentId",
                table: "Relacion_Estudio_Reactivo",
                column: "ReagentId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_WorkList_WorkListId",
                table: "Relacion_Estudio_WorkList",
                column: "WorkListId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_Cat_Formato_FormatoId",
                table: "CAT_Estudio",
                column: "FormatoId",
                principalTable: "Cat_Formato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_Maquilador_MaquiladorId",
                table: "CAT_Estudio",
                column: "MaquiladorId",
                principalTable: "CAT_Maquilador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_Metodo_MetodoId",
                table: "CAT_Estudio",
                column: "MetodoId",
                principalTable: "CAT_Metodo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_Tipo_Tapon_TaponId",
                table: "CAT_Estudio",
                column: "TaponId",
                principalTable: "CAT_Tipo_Tapon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_TipoMuestra_SampleTypeId",
                table: "CAT_Estudio",
                column: "SampleTypeId",
                principalTable: "CAT_TipoMuestra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_Cat_Formato_FormatoId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_Maquilador_MaquiladorId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_Metodo_MetodoId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_Tipo_Tapon_TaponId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_TipoMuestra_SampleTypeId",
                table: "CAT_Estudio");

            migrationBuilder.DropTable(
                name: "CAT_Tipo_Tapon");

            migrationBuilder.DropTable(
                name: "Relacion_Estudio_Paquete");

            migrationBuilder.DropTable(
                name: "Relacion_Estudio_Reactivo");

            migrationBuilder.DropTable(
                name: "Relacion_Estudio_WorkList");

            migrationBuilder.DropTable(
                name: "Packet");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Estudio_FormatoId",
                table: "CAT_Estudio");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Estudio_MaquiladorId",
                table: "CAT_Estudio");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Estudio_MetodoId",
                table: "CAT_Estudio");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Estudio_SampleTypeId",
                table: "CAT_Estudio");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Estudio_TaponId",
                table: "CAT_Estudio");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "CAT_Estudio");

            migrationBuilder.DropColumn(
                name: "Dias",
                table: "CAT_Estudio");

            migrationBuilder.DropColumn(
                name: "SampleTypeId",
                table: "CAT_Estudio");

            migrationBuilder.DropColumn(
                name: "Urgencia",
                table: "CAT_Estudio");

            migrationBuilder.RenameColumn(
                name: "TiempoResultado",
                table: "CAT_Estudio",
                newName: "TipoMuestraId");

            migrationBuilder.RenameColumn(
                name: "TaponId",
                table: "CAT_Estudio",
                newName: "TiempoRespuesta");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioModId",
                table: "CAT_Estudio",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioCreoId",
                table: "CAT_Estudio",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "CAT_Estudio",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Prioridad",
                table: "CAT_Estudio",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "NombreCorto",
                table: "CAT_Estudio",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "CAT_Estudio",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaMod",
                table: "CAT_Estudio",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreo",
                table: "CAT_Estudio",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Clave",
                table: "CAT_Estudio",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
