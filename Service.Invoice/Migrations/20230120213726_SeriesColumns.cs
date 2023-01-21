using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Billing.Migrations
{
    public partial class SeriesColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CFDI",
                table: "CAT_Serie",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "CAT_Serie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sucursal",
                table: "CAT_Serie",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CFDI",
                table: "CAT_Serie");

            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "CAT_Serie");

            migrationBuilder.DropColumn(
                name: "Sucursal",
                table: "CAT_Serie");
        }
    }
}
