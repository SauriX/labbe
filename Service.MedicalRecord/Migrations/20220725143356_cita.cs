using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class cita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cita",
                table: "CAT_Cita_Lab",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cita",
                table: "CAT_Cita_Dom",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cita",
                table: "CAT_Cita_Lab");

            migrationBuilder.DropColumn(
                name: "Cita",
                table: "CAT_Cita_Dom");
        }
    }
}
