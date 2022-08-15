using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class TablaSolicituddefaultvalueparaProcedencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Procedencia",
                table: "CAT_Solicitud",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)2,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValue: (byte)1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Procedencia",
                table: "CAT_Solicitud",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)1,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValue: (byte)2);
        }
    }
}
