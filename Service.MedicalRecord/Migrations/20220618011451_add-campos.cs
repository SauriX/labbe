using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class addcampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "CAT_Expedientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoPostal",
                table: "CAT_Expedientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColoniaId",
                table: "CAT_Expedientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "CAT_Expedientes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "CAT_Expedientes");

            migrationBuilder.DropColumn(
                name: "CodigoPostal",
                table: "CAT_Expedientes");

            migrationBuilder.DropColumn(
                name: "ColoniaId",
                table: "CAT_Expedientes");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "CAT_Expedientes");
        }
    }
}
