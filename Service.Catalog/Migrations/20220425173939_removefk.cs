using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class removefk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartamentoId",
                table: "CAT_Parametro",
                newName: "DepartamentId");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "CAT_Parametro",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Telefono",
                table: "CAT_CompañiaContacto",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Parametro_DepartmentId",
                table: "CAT_Parametro",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_CAT_Departamento_DepartmentId",
                table: "CAT_Parametro",
                column: "DepartmentId",
                principalTable: "CAT_Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_CAT_Departamento_DepartmentId",
                table: "CAT_Parametro");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Parametro_DepartmentId",
                table: "CAT_Parametro");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "CAT_Parametro");

            migrationBuilder.RenameColumn(
                name: "DepartamentId",
                table: "CAT_Parametro",
                newName: "DepartamentoId");

            migrationBuilder.AlterColumn<int>(
                name: "Telefono",
                table: "CAT_CompañiaContacto",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
