﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class changefk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CAT_Parametro_AreaId",
                table: "CAT_Parametro");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Parametro_ReagentId",
                table: "CAT_Parametro");

            migrationBuilder.AlterColumn<long>(
                name: "Telefono",
                table: "CAT_CompañiaContacto",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visibilidad = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDeDescuento = table.Column<bool>(type: "bit", nullable: false),
                    CantidadDescuento = table.Column<float>(type: "real", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Visibilidad = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAT_ListaP_Compañia",
                columns: table => new
                {
                    CompañiaId = table.Column<int>(type: "int", nullable: false),
                    PrecioId = table.Column<int>(type: "int", nullable: false),
                    ListaPrecioId = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ListaP_Compañia", x => new { x.PrecioId, x.CompañiaId });
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Compañia_CAT_Compañia_CompañiaId",
                        column: x => x.CompañiaId,
                        principalTable: "CAT_Compañia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Compañia_Price_ListaPrecioId",
                        column: x => x.ListaPrecioId,
                        principalTable: "Price",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CAT_ListaP_Promocion",
                columns: table => new
                {
                    PrecioId = table.Column<int>(type: "int", nullable: false),
                    PromocionId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_ListaP_Promocion", x => new { x.PrecioId, x.PromocionId });
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Promocion_Price_PrecioId",
                        column: x => x.PrecioId,
                        principalTable: "Price",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CAT_ListaP_Promocion_Promotion_PromocionId",
                        column: x => x.PromocionId,
                        principalTable: "Promotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_AreaId",
                table: "CAT_Parametro",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_ReagentId",
                table: "CAT_Parametro",
                column: "ReagentId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaP_Compañia_CompañiaId",
                table: "CAT_ListaP_Compañia",
                column: "CompañiaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaP_Compañia_ListaPrecioId",
                table: "CAT_ListaP_Compañia",
                column: "ListaPrecioId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaP_Promocion_PromocionId",
                table: "CAT_ListaP_Promocion",
                column: "PromocionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_ListaP_Compañia");

            migrationBuilder.DropTable(
                name: "CAT_ListaP_Promocion");

            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Parametro_AreaId",
                table: "CAT_Parametro");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Parametro_ReagentId",
                table: "CAT_Parametro");

            migrationBuilder.AlterColumn<long>(
                name: "Telefono",
                table: "CAT_CompañiaContacto",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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
        }
    }
}
