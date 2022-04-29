using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CAT_MaquiladorCorreccion3yCodigopostal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CiudadId",
                table: "CAT_Maquilador");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "CAT_Maquilador");

            migrationBuilder.AlterColumn<int>(
                name: "ColoniaId",
                table: "CAT_Maquilador",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Maquilador_ColoniaId",
                table: "CAT_Maquilador",
                column: "ColoniaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Maquilador_CAT_Colonia_ColoniaId",
                table: "CAT_Maquilador",
                column: "ColoniaId",
                principalTable: "CAT_Colonia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Maquilador_CAT_Colonia_ColoniaId",
                table: "CAT_Maquilador");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Maquilador_ColoniaId",
                table: "CAT_Maquilador");

            migrationBuilder.AlterColumn<long>(
                name: "ColoniaId",
                table: "CAT_Maquilador",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<long>(
                name: "CiudadId",
                table: "CAT_Maquilador",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EstadoId",
                table: "CAT_Maquilador",
                type: "bigint",
                nullable: true);
        }
    }
}
