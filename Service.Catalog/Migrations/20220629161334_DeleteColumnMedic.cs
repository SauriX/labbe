using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class DeleteColumnMedic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CiudadId",
                table: "CAT_Medico");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "CAT_Medico");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CiudadId",
                table: "CAT_Medico",
                type: "bigint",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EstadoId",
                table: "CAT_Medico",
                type: "bigint",
                maxLength: 15,
                nullable: true);
        }
    }
}
