using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class ModificacionMaquilaColoniaIdNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Maquilador_CAT_Colonia_ColoniaId",
                table: "CAT_Maquilador");

            migrationBuilder.AlterColumn<int>(
                name: "ColoniaId",
                table: "CAT_Maquilador",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Maquilador_CAT_Colonia_ColoniaId",
                table: "CAT_Maquilador",
                column: "ColoniaId",
                principalTable: "CAT_Colonia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Maquilador_CAT_Colonia_ColoniaId",
                table: "CAT_Maquilador");

            migrationBuilder.AlterColumn<int>(
                name: "ColoniaId",
                table: "CAT_Maquilador",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Maquilador_CAT_Colonia_ColoniaId",
                table: "CAT_Maquilador",
                column: "ColoniaId",
                principalTable: "CAT_Colonia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
