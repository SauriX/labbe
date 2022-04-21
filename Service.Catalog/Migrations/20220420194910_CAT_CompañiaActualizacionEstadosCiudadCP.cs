using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CAT_CompañiaActualizacionEstadosCiudadCP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstadoId",
                table: "CAT_Compañia",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "CiudadId",
                table: "CAT_Compañia",
                newName: "Ciudad");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoPostal",
                table: "CAT_Compañia",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "CAT_Compañia",
                newName: "EstadoId");

            migrationBuilder.RenameColumn(
                name: "Ciudad",
                table: "CAT_Compañia",
                newName: "CiudadId");

            migrationBuilder.AlterColumn<int>(
                name: "CodigoPostal",
                table: "CAT_Compañia",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
