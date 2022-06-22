using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class BorrarLPPromoEnCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListaPrecioId",
                table: "CAT_Compañia");

            migrationBuilder.DropColumn(
                name: "PromocionesId",
                table: "CAT_Compañia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListaPrecioId",
                table: "CAT_Compañia",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromocionesId",
                table: "CAT_Compañia",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
