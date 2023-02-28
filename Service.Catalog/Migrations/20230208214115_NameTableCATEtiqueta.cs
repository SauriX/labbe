using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class NameTableCATEtiqueta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_Tipo_Tapon_TaponId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Estudio_Etiqueta_CAT_Tipo_Tapon_EtiquetaId",
                table: "Relacion_Estudio_Etiqueta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_Tipo_Tapon",
                table: "CAT_Tipo_Tapon");

            migrationBuilder.RenameTable(
                name: "CAT_Tipo_Tapon",
                newName: "CAT_Etiqueta");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_Etiqueta",
                table: "CAT_Etiqueta",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_Etiqueta_TaponId",
                table: "CAT_Estudio",
                column: "TaponId",
                principalTable: "CAT_Etiqueta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Estudio_Etiqueta_CAT_Etiqueta_EtiquetaId",
                table: "Relacion_Estudio_Etiqueta",
                column: "EtiquetaId",
                principalTable: "CAT_Etiqueta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Estudio_CAT_Etiqueta_TaponId",
                table: "CAT_Estudio");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Estudio_Etiqueta_CAT_Etiqueta_EtiquetaId",
                table: "Relacion_Estudio_Etiqueta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_Etiqueta",
                table: "CAT_Etiqueta");

            migrationBuilder.RenameTable(
                name: "CAT_Etiqueta",
                newName: "CAT_Tipo_Tapon");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_Tipo_Tapon",
                table: "CAT_Tipo_Tapon",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Estudio_CAT_Tipo_Tapon_TaponId",
                table: "CAT_Estudio",
                column: "TaponId",
                principalTable: "CAT_Tipo_Tapon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Estudio_Etiqueta_CAT_Tipo_Tapon_EtiquetaId",
                table: "Relacion_Estudio_Etiqueta",
                column: "EtiquetaId",
                principalTable: "CAT_Tipo_Tapon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
