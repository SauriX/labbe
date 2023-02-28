using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class Cascade_Soliciutdes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Pago_CAT_Solicitud_SolicitudId",
                table: "Relacion_Solicitud_Pago");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Pago_CAT_Solicitud_SolicitudId",
                table: "Relacion_Solicitud_Pago",
                column: "SolicitudId",
                principalTable: "CAT_Solicitud",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Pago_CAT_Solicitud_SolicitudId",
                table: "Relacion_Solicitud_Pago");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Pago_CAT_Solicitud_SolicitudId",
                table: "Relacion_Solicitud_Pago",
                column: "SolicitudId",
                principalTable: "CAT_Solicitud",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
