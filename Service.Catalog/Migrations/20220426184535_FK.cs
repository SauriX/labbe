using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EspecialidadId",
                table: "CAT_Medico",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "ColoniaId",
                table: "CAT_Medico",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 15);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Medico_ColoniaId",
                table: "CAT_Medico",
                column: "ColoniaId");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Medico_EspecialidadId",
                table: "CAT_Medico",
                column: "EspecialidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Medico_CAT_Colonia_ColoniaId",
                table: "CAT_Medico",
                column: "ColoniaId",
                principalTable: "CAT_Colonia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Medico_CAT_Especialidad_EspecialidadId",
                table: "CAT_Medico",
                column: "EspecialidadId",
                principalTable: "CAT_Especialidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Medico_CAT_Colonia_ColoniaId",
                table: "CAT_Medico");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Medico_CAT_Especialidad_EspecialidadId",
                table: "CAT_Medico");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Medico_ColoniaId",
                table: "CAT_Medico");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Medico_EspecialidadId",
                table: "CAT_Medico");

            migrationBuilder.AlterColumn<long>(
                name: "EspecialidadId",
                table: "CAT_Medico",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "ColoniaId",
                table: "CAT_Medico",
                type: "bigint",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
