using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class CAT_CompañiaActualizacionEstadosCiudad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MunicipioId",
                table: "CAT_Compañia");

            migrationBuilder.AlterColumn<string>(
                name: "EstadoId",
                table: "CAT_Compañia",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CiudadId",
                table: "CAT_Compañia",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CiudadId",
                table: "CAT_Compañia");

            migrationBuilder.AlterColumn<int>(
                name: "EstadoId",
                table: "CAT_Compañia",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MunicipioId",
                table: "CAT_Compañia",
                type: "int",
                nullable: true);
        }
    }
}
