using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Report.Migrations
{
    public partial class AgregarcolumnaConvenio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Descuento",
                table: "RequestStudy",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<byte>(
                name: "Convenio",
                table: "Company",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Convenio",
                table: "Company");

            migrationBuilder.AlterColumn<bool>(
                name: "Descuento",
                table: "RequestStudy",
                type: "bit",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
