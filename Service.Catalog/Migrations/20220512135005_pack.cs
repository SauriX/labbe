using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class pack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_ListaP_Promocion_CAT_ListaPrecio_PriceId",
                table: "CAT_ListaP_Promocion");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Estudio_Paquete_CAT_Estudio_EstudioId",
                table: "Relacion_Estudio_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Estudio_Paquete_Packet_PacketId",
                table: "Relacion_Estudio_Paquete");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Packet",
                table: "Packet");

            migrationBuilder.RenameTable(
                name: "Packet",
                newName: "CAT_Paquete");

            migrationBuilder.RenameColumn(
                name: "PriceId",
                table: "CAT_ListaP_Promocion",
                newName: "PriceListId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_ListaP_Promocion_PriceId",
                table: "CAT_ListaP_Promocion",
                newName: "IX_CAT_ListaP_Promocion_PriceListId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioModificoId",
                table: "CAT_ListaPrecio",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioCreoId",
                table: "CAT_ListaPrecio",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "CAT_Paquete",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Clave",
                table: "CAT_Paquete",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "CAT_Paquete",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "CAT_Paquete",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NombreLargo",
                table: "CAT_Paquete",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Visibilidad",
                table: "CAT_Paquete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_Paquete",
                table: "CAT_Paquete",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Relacion_ListaP_Estudio",
                columns: table => new
                {
                    PrecioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstudioId = table.Column<int>(type: "int", nullable: false),
                    PrecioId1 = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_ListaP_Estudio", x => new { x.PrecioId, x.EstudioId });
                    table.ForeignKey(
                        name: "FK_Relacion_ListaP_Estudio_CAT_Estudio_EstudioId",
                        column: x => x.EstudioId,
                        principalTable: "CAT_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_ListaP_Estudio_CAT_ListaPrecio_PrecioId1",
                        column: x => x.PrecioId1,
                        principalTable: "CAT_ListaPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relacion_ListaP_Estudio_CAT_ListaPrecio_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "CAT_ListaPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Relacion_ListaP_Paquete",
                columns: table => new
                {
                    PrecioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaqueteId = table.Column<int>(type: "int", nullable: false),
                    PrecioId1 = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_ListaP_Paquete", x => new { x.PrecioId, x.PaqueteId });
                    table.ForeignKey(
                        name: "FK_Relacion_ListaP_Paquete_CAT_ListaPrecio_PrecioId1",
                        column: x => x.PrecioId1,
                        principalTable: "CAT_ListaPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relacion_ListaP_Paquete_CAT_ListaPrecio_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "CAT_ListaPrecio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relacion_ListaP_Paquete_CAT_Paquete_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "CAT_Paquete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Paquete_AreaId",
                table: "CAT_Paquete",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_ListaP_Estudio_EstudioId",
                table: "Relacion_ListaP_Estudio",
                column: "EstudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_ListaP_Estudio_PrecioId1",
                table: "Relacion_ListaP_Estudio",
                column: "PrecioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_ListaP_Estudio_PriceListId",
                table: "Relacion_ListaP_Estudio",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_ListaP_Paquete_PaqueteId",
                table: "Relacion_ListaP_Paquete",
                column: "PaqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_ListaP_Paquete_PrecioId1",
                table: "Relacion_ListaP_Paquete",
                column: "PrecioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_ListaP_Paquete_PriceListId",
                table: "Relacion_ListaP_Paquete",
                column: "PriceListId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_ListaP_Promocion_CAT_ListaPrecio_PriceListId",
                table: "CAT_ListaP_Promocion",
                column: "PriceListId",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Paquete_CAT_Area_AreaId",
                table: "CAT_Paquete",
                column: "AreaId",
                principalTable: "CAT_Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Estudio_Paquete_CAT_Estudio_EstudioId",
                table: "Relacion_Estudio_Paquete",
                column: "EstudioId",
                principalTable: "CAT_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Estudio_Paquete_CAT_Paquete_PacketId",
                table: "Relacion_Estudio_Paquete",
                column: "PacketId",
                principalTable: "CAT_Paquete",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_ListaP_Promocion_CAT_ListaPrecio_PriceListId",
                table: "CAT_ListaP_Promocion");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Paquete_CAT_Area_AreaId",
                table: "CAT_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Estudio_Paquete_CAT_Estudio_EstudioId",
                table: "Relacion_Estudio_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Estudio_Paquete_CAT_Paquete_PacketId",
                table: "Relacion_Estudio_Paquete");

            migrationBuilder.DropTable(
                name: "Relacion_ListaP_Estudio");

            migrationBuilder.DropTable(
                name: "Relacion_ListaP_Paquete");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_Paquete",
                table: "CAT_Paquete");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Paquete_AreaId",
                table: "CAT_Paquete");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "CAT_Paquete");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "CAT_Paquete");

            migrationBuilder.DropColumn(
                name: "NombreLargo",
                table: "CAT_Paquete");

            migrationBuilder.DropColumn(
                name: "Visibilidad",
                table: "CAT_Paquete");

            migrationBuilder.RenameTable(
                name: "CAT_Paquete",
                newName: "Packet");

            migrationBuilder.RenameColumn(
                name: "PriceListId",
                table: "CAT_ListaP_Promocion",
                newName: "PriceId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_ListaP_Promocion_PriceListId",
                table: "CAT_ListaP_Promocion",
                newName: "IX_CAT_ListaP_Promocion_PriceId");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioModificoId",
                table: "CAT_ListaPrecio",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioCreoId",
                table: "CAT_ListaPrecio",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Packet",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Clave",
                table: "Packet",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Packet",
                table: "Packet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_ListaP_Promocion_CAT_ListaPrecio_PriceId",
                table: "CAT_ListaP_Promocion",
                column: "PriceId",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Estudio_Paquete_CAT_Estudio_EstudioId",
                table: "Relacion_Estudio_Paquete",
                column: "EstudioId",
                principalTable: "CAT_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Estudio_Paquete_Packet_PacketId",
                table: "Relacion_Estudio_Paquete",
                column: "PacketId",
                principalTable: "Packet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
