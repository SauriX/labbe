using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class PROMOCIONES : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Estudio_CAT_Estudio_StudyId",
                table: "Relacion_Promocion_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Estudio_CAT_Promocion_PromotionId",
                table: "Relacion_Promocion_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Medicos_CAT_Medico_MedicId",
                table: "Relacion_Promocion_Medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Medicos_CAT_Promocion_PromotionId",
                table: "Relacion_Promocion_Medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Paquete_CAT_Paquete_PackId",
                table: "Relacion_Promocion_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Paquete_CAT_Promocion_PromotionId",
                table: "Relacion_Promocion_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Sucursal_CAT_Promocion_PromotionId",
                table: "Relacion_Promocion_Sucursal");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Sucursal_CAT_Sucursal_BranchId",
                table: "Relacion_Promocion_Sucursal");

            migrationBuilder.DropTable(
                name: "Relacion_Promocion_Lealtad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Relacion_Promocion_Sucursal",
                table: "Relacion_Promocion_Sucursal");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Promocion_Sucursal_BranchId",
                table: "Relacion_Promocion_Sucursal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Relacion_Promocion_Medicos",
                table: "Relacion_Promocion_Medicos");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Promocion_Medicos_MedicId",
                table: "Relacion_Promocion_Medicos");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Relacion_Promocion_Sucursal");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Relacion_Promocion_Sucursal");

            migrationBuilder.DropColumn(
                name: "Loyality",
                table: "Relacion_Promocion_Paquete");

            migrationBuilder.DropColumn(
                name: "UsuarioModId",
                table: "Relacion_Promocion_Paquete");

            migrationBuilder.DropColumn(
                name: "MedicId",
                table: "Relacion_Promocion_Medicos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Relacion_Promocion_Medicos");

            migrationBuilder.DropColumn(
                name: "Loyality",
                table: "Relacion_Promocion_Estudio");

            migrationBuilder.DropColumn(
                name: "UsuarioModId",
                table: "Relacion_Promocion_Estudio");

            migrationBuilder.RenameColumn(
                name: "UsuarioModId",
                table: "Relacion_Promocion_Sucursal",
                newName: "SucursalId");

            migrationBuilder.RenameColumn(
                name: "FechaMod",
                table: "Relacion_Promocion_Sucursal",
                newName: "FechaModifico");

            migrationBuilder.RenameColumn(
                name: "PromotionId",
                table: "Relacion_Promocion_Sucursal",
                newName: "PromocionId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Relacion_Promocion_Paquete",
                newName: "PrecioFinal");

            migrationBuilder.RenameColumn(
                name: "FinalPrice",
                table: "Relacion_Promocion_Paquete",
                newName: "Precio");

            migrationBuilder.RenameColumn(
                name: "FechaMod",
                table: "Relacion_Promocion_Paquete",
                newName: "FechaModifico");

            migrationBuilder.RenameColumn(
                name: "FechaInicio",
                table: "Relacion_Promocion_Paquete",
                newName: "FechaInicial");

            migrationBuilder.RenameColumn(
                name: "Discountporcent",
                table: "Relacion_Promocion_Paquete",
                newName: "DescuentoPorcentaje");

            migrationBuilder.RenameColumn(
                name: "DiscountNumeric",
                table: "Relacion_Promocion_Paquete",
                newName: "DescuentoCantidad");

            migrationBuilder.RenameColumn(
                name: "PackId",
                table: "Relacion_Promocion_Paquete",
                newName: "PaqueteId");

            migrationBuilder.RenameColumn(
                name: "PromotionId",
                table: "Relacion_Promocion_Paquete",
                newName: "PromocionId");

            migrationBuilder.RenameIndex(
                name: "IX_Relacion_Promocion_Paquete_PackId",
                table: "Relacion_Promocion_Paquete",
                newName: "IX_Relacion_Promocion_Paquete_PaqueteId");

            migrationBuilder.RenameColumn(
                name: "UsuarioModId",
                table: "Relacion_Promocion_Medicos",
                newName: "MedicoId");

            migrationBuilder.RenameColumn(
                name: "FechaMod",
                table: "Relacion_Promocion_Medicos",
                newName: "FechaModifico");

            migrationBuilder.RenameColumn(
                name: "PromotionId",
                table: "Relacion_Promocion_Medicos",
                newName: "PromocionId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Relacion_Promocion_Estudio",
                newName: "PrecioFinal");

            migrationBuilder.RenameColumn(
                name: "FinalPrice",
                table: "Relacion_Promocion_Estudio",
                newName: "Precio");

            migrationBuilder.RenameColumn(
                name: "FechaMod",
                table: "Relacion_Promocion_Estudio",
                newName: "FechaModifico");

            migrationBuilder.RenameColumn(
                name: "FechaInicio",
                table: "Relacion_Promocion_Estudio",
                newName: "FechaInicial");

            migrationBuilder.RenameColumn(
                name: "Discountporcent",
                table: "Relacion_Promocion_Estudio",
                newName: "DescuentoPorcentaje");

            migrationBuilder.RenameColumn(
                name: "DiscountNumeric",
                table: "Relacion_Promocion_Estudio",
                newName: "DescuentoCantidad");

            migrationBuilder.RenameColumn(
                name: "StudyId",
                table: "Relacion_Promocion_Estudio",
                newName: "EstudioId");

            migrationBuilder.RenameColumn(
                name: "PromotionId",
                table: "Relacion_Promocion_Estudio",
                newName: "PromocionId");

            migrationBuilder.RenameIndex(
                name: "IX_Relacion_Promocion_Estudio_StudyId",
                table: "Relacion_Promocion_Estudio",
                newName: "IX_Relacion_Promocion_Estudio_EstudioId");

            migrationBuilder.RenameColumn(
                name: "Visibilidad",
                table: "CAT_Promocion",
                newName: "AplicaMedicos");

            migrationBuilder.RenameColumn(
                name: "PrecioListaId",
                table: "CAT_Promocion",
                newName: "ListaPrecioId");

            migrationBuilder.RenameColumn(
                name: "FechaInicio",
                table: "CAT_Promocion",
                newName: "FechaInicial");

            migrationBuilder.RenameColumn(
                name: "CantidadDescuento",
                table: "CAT_Promocion",
                newName: "Cantidad");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioModificoId",
                table: "Relacion_Promocion_Sucursal",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioModificoId",
                table: "Relacion_Promocion_Paquete",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioModificoId",
                table: "Relacion_Promocion_Medicos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioModificoId",
                table: "Relacion_Promocion_Estudio",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioModificoId",
                table: "CAT_Promocion",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioCreoId",
                table: "CAT_Promocion",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relacion_Promocion_Sucursal",
                table: "Relacion_Promocion_Sucursal",
                columns: new[] { "PromocionId", "SucursalId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relacion_Promocion_Medicos",
                table: "Relacion_Promocion_Medicos",
                columns: new[] { "PromocionId", "MedicoId" });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Promocion_Sucursal_SucursalId",
                table: "Relacion_Promocion_Sucursal",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Promocion_Medicos_MedicoId",
                table: "Relacion_Promocion_Medicos",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Promocion_ListaPrecioId",
                table: "CAT_Promocion",
                column: "ListaPrecioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Promocion_CAT_ListaPrecio_ListaPrecioId",
                table: "CAT_Promocion",
                column: "ListaPrecioId",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Estudio_CAT_Estudio_EstudioId",
                table: "Relacion_Promocion_Estudio",
                column: "EstudioId",
                principalTable: "CAT_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Estudio_CAT_Promocion_PromocionId",
                table: "Relacion_Promocion_Estudio",
                column: "PromocionId",
                principalTable: "CAT_Promocion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Medicos_CAT_Medico_MedicoId",
                table: "Relacion_Promocion_Medicos",
                column: "MedicoId",
                principalTable: "CAT_Medico",
                principalColumn: "IdMedico",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Medicos_CAT_Promocion_PromocionId",
                table: "Relacion_Promocion_Medicos",
                column: "PromocionId",
                principalTable: "CAT_Promocion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Paquete_CAT_Paquete_PaqueteId",
                table: "Relacion_Promocion_Paquete",
                column: "PaqueteId",
                principalTable: "CAT_Paquete",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Paquete_CAT_Promocion_PromocionId",
                table: "Relacion_Promocion_Paquete",
                column: "PromocionId",
                principalTable: "CAT_Promocion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Sucursal_CAT_Promocion_PromocionId",
                table: "Relacion_Promocion_Sucursal",
                column: "PromocionId",
                principalTable: "CAT_Promocion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Sucursal_CAT_Sucursal_SucursalId",
                table: "Relacion_Promocion_Sucursal",
                column: "SucursalId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Promocion_CAT_ListaPrecio_ListaPrecioId",
                table: "CAT_Promocion");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Estudio_CAT_Estudio_EstudioId",
                table: "Relacion_Promocion_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Estudio_CAT_Promocion_PromocionId",
                table: "Relacion_Promocion_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Medicos_CAT_Medico_MedicoId",
                table: "Relacion_Promocion_Medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Medicos_CAT_Promocion_PromocionId",
                table: "Relacion_Promocion_Medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Paquete_CAT_Paquete_PaqueteId",
                table: "Relacion_Promocion_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Paquete_CAT_Promocion_PromocionId",
                table: "Relacion_Promocion_Paquete");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Sucursal_CAT_Promocion_PromocionId",
                table: "Relacion_Promocion_Sucursal");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Promocion_Sucursal_CAT_Sucursal_SucursalId",
                table: "Relacion_Promocion_Sucursal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Relacion_Promocion_Sucursal",
                table: "Relacion_Promocion_Sucursal");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Promocion_Sucursal_SucursalId",
                table: "Relacion_Promocion_Sucursal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Relacion_Promocion_Medicos",
                table: "Relacion_Promocion_Medicos");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Promocion_Medicos_MedicoId",
                table: "Relacion_Promocion_Medicos");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Promocion_ListaPrecioId",
                table: "CAT_Promocion");

            migrationBuilder.DropColumn(
                name: "UsuarioModificoId",
                table: "Relacion_Promocion_Sucursal");

            migrationBuilder.DropColumn(
                name: "UsuarioModificoId",
                table: "Relacion_Promocion_Paquete");

            migrationBuilder.DropColumn(
                name: "UsuarioModificoId",
                table: "Relacion_Promocion_Medicos");

            migrationBuilder.DropColumn(
                name: "UsuarioModificoId",
                table: "Relacion_Promocion_Estudio");

            migrationBuilder.RenameColumn(
                name: "FechaModifico",
                table: "Relacion_Promocion_Sucursal",
                newName: "FechaMod");

            migrationBuilder.RenameColumn(
                name: "SucursalId",
                table: "Relacion_Promocion_Sucursal",
                newName: "UsuarioModId");

            migrationBuilder.RenameColumn(
                name: "PromocionId",
                table: "Relacion_Promocion_Sucursal",
                newName: "PromotionId");

            migrationBuilder.RenameColumn(
                name: "PrecioFinal",
                table: "Relacion_Promocion_Paquete",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "Relacion_Promocion_Paquete",
                newName: "FinalPrice");

            migrationBuilder.RenameColumn(
                name: "FechaModifico",
                table: "Relacion_Promocion_Paquete",
                newName: "FechaMod");

            migrationBuilder.RenameColumn(
                name: "FechaInicial",
                table: "Relacion_Promocion_Paquete",
                newName: "FechaInicio");

            migrationBuilder.RenameColumn(
                name: "DescuentoPorcentaje",
                table: "Relacion_Promocion_Paquete",
                newName: "Discountporcent");

            migrationBuilder.RenameColumn(
                name: "DescuentoCantidad",
                table: "Relacion_Promocion_Paquete",
                newName: "DiscountNumeric");

            migrationBuilder.RenameColumn(
                name: "PaqueteId",
                table: "Relacion_Promocion_Paquete",
                newName: "PackId");

            migrationBuilder.RenameColumn(
                name: "PromocionId",
                table: "Relacion_Promocion_Paquete",
                newName: "PromotionId");

            migrationBuilder.RenameIndex(
                name: "IX_Relacion_Promocion_Paquete_PaqueteId",
                table: "Relacion_Promocion_Paquete",
                newName: "IX_Relacion_Promocion_Paquete_PackId");

            migrationBuilder.RenameColumn(
                name: "FechaModifico",
                table: "Relacion_Promocion_Medicos",
                newName: "FechaMod");

            migrationBuilder.RenameColumn(
                name: "MedicoId",
                table: "Relacion_Promocion_Medicos",
                newName: "UsuarioModId");

            migrationBuilder.RenameColumn(
                name: "PromocionId",
                table: "Relacion_Promocion_Medicos",
                newName: "PromotionId");

            migrationBuilder.RenameColumn(
                name: "PrecioFinal",
                table: "Relacion_Promocion_Estudio",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "Relacion_Promocion_Estudio",
                newName: "FinalPrice");

            migrationBuilder.RenameColumn(
                name: "FechaModifico",
                table: "Relacion_Promocion_Estudio",
                newName: "FechaMod");

            migrationBuilder.RenameColumn(
                name: "FechaInicial",
                table: "Relacion_Promocion_Estudio",
                newName: "FechaInicio");

            migrationBuilder.RenameColumn(
                name: "DescuentoPorcentaje",
                table: "Relacion_Promocion_Estudio",
                newName: "Discountporcent");

            migrationBuilder.RenameColumn(
                name: "DescuentoCantidad",
                table: "Relacion_Promocion_Estudio",
                newName: "DiscountNumeric");

            migrationBuilder.RenameColumn(
                name: "EstudioId",
                table: "Relacion_Promocion_Estudio",
                newName: "StudyId");

            migrationBuilder.RenameColumn(
                name: "PromocionId",
                table: "Relacion_Promocion_Estudio",
                newName: "PromotionId");

            migrationBuilder.RenameIndex(
                name: "IX_Relacion_Promocion_Estudio_EstudioId",
                table: "Relacion_Promocion_Estudio",
                newName: "IX_Relacion_Promocion_Estudio_StudyId");

            migrationBuilder.RenameColumn(
                name: "ListaPrecioId",
                table: "CAT_Promocion",
                newName: "PrecioListaId");

            migrationBuilder.RenameColumn(
                name: "FechaInicial",
                table: "CAT_Promocion",
                newName: "FechaInicio");

            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "CAT_Promocion",
                newName: "CantidadDescuento");

            migrationBuilder.RenameColumn(
                name: "AplicaMedicos",
                table: "CAT_Promocion",
                newName: "Visibilidad");

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "Relacion_Promocion_Sucursal",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Relacion_Promocion_Sucursal",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Loyality",
                table: "Relacion_Promocion_Paquete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioModId",
                table: "Relacion_Promocion_Paquete",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MedicId",
                table: "Relacion_Promocion_Medicos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Relacion_Promocion_Medicos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Loyality",
                table: "Relacion_Promocion_Estudio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioModId",
                table: "Relacion_Promocion_Estudio",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioModificoId",
                table: "CAT_Promocion",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioCreoId",
                table: "CAT_Promocion",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relacion_Promocion_Sucursal",
                table: "Relacion_Promocion_Sucursal",
                columns: new[] { "PromotionId", "BranchId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relacion_Promocion_Medicos",
                table: "Relacion_Promocion_Medicos",
                columns: new[] { "PromotionId", "MedicId" });

            migrationBuilder.CreateTable(
                name: "Relacion_Promocion_Lealtad",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    LoyaltyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioModId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Promocion_Lealtad", x => new { x.PromotionId, x.LoyaltyId });
                    table.ForeignKey(
                        name: "FK_Relacion_Promocion_Lealtad_CAT_Lealtad_LoyaltyId",
                        column: x => x.LoyaltyId,
                        principalTable: "CAT_Lealtad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacion_Promocion_Lealtad_CAT_Promocion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "CAT_Promocion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Promocion_Sucursal_BranchId",
                table: "Relacion_Promocion_Sucursal",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Promocion_Medicos_MedicId",
                table: "Relacion_Promocion_Medicos",
                column: "MedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Promocion_Lealtad_LoyaltyId",
                table: "Relacion_Promocion_Lealtad",
                column: "LoyaltyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Estudio_CAT_Estudio_StudyId",
                table: "Relacion_Promocion_Estudio",
                column: "StudyId",
                principalTable: "CAT_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Estudio_CAT_Promocion_PromotionId",
                table: "Relacion_Promocion_Estudio",
                column: "PromotionId",
                principalTable: "CAT_Promocion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Medicos_CAT_Medico_MedicId",
                table: "Relacion_Promocion_Medicos",
                column: "MedicId",
                principalTable: "CAT_Medico",
                principalColumn: "IdMedico",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Medicos_CAT_Promocion_PromotionId",
                table: "Relacion_Promocion_Medicos",
                column: "PromotionId",
                principalTable: "CAT_Promocion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Paquete_CAT_Paquete_PackId",
                table: "Relacion_Promocion_Paquete",
                column: "PackId",
                principalTable: "CAT_Paquete",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Paquete_CAT_Promocion_PromotionId",
                table: "Relacion_Promocion_Paquete",
                column: "PromotionId",
                principalTable: "CAT_Promocion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Sucursal_CAT_Promocion_PromotionId",
                table: "Relacion_Promocion_Sucursal",
                column: "PromotionId",
                principalTable: "CAT_Promocion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Promocion_Sucursal_CAT_Sucursal_BranchId",
                table: "Relacion_Promocion_Sucursal",
                column: "BranchId",
                principalTable: "CAT_Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
