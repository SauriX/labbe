using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class addcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Calle",
                table: "CAT_Datos_Fiscales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "CAT_Datos_Fiscales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoPostal",
                table: "CAT_Datos_Fiscales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "CAT_Datos_Fiscales",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calle",
                table: "CAT_Datos_Fiscales");

            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "CAT_Datos_Fiscales");

            migrationBuilder.DropColumn(
                name: "CodigoPostal",
                table: "CAT_Datos_Fiscales");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "CAT_Datos_Fiscales");
        }
    }
}
