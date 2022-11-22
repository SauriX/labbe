using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class RequestStudycamposnuleables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_CAT_Tipo_Tapon_TaponId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.AlterColumn<int>(
                name: "TaponId",
                table: "Relacion_Solicitud_Estudio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "Relacion_Solicitud_Estudio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "Relacion_Solicitud_Estudio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_CAT_Tipo_Tapon_TaponId",
                table: "Relacion_Solicitud_Estudio",
                column: "TaponId",
                principalTable: "CAT_Tipo_Tapon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_CAT_Tipo_Tapon_TaponId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.AlterColumn<int>(
                name: "TaponId",
                table: "Relacion_Solicitud_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "Relacion_Solicitud_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "Relacion_Solicitud_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_CAT_Tipo_Tapon_TaponId",
                table: "Relacion_Solicitud_Estudio",
                column: "TaponId",
                principalTable: "CAT_Tipo_Tapon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
