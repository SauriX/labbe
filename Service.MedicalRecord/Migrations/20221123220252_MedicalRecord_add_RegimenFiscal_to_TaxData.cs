using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class MedicalRecord_add_RegimenFiscal_to_TaxData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegimenFiscal",
                table: "CAT_Datos_Fiscales",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegimenFiscal",
                table: "CAT_Datos_Fiscales");
        }
    }
}
