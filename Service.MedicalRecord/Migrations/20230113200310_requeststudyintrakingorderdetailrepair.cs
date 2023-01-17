using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class requeststudyintrakingorderdetailrepair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SolicitudEstudioId",
                table: "Relacion_Seguimiento_Solicitud",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Seguimiento_Solicitud_SolicitudEstudioId",
                table: "Relacion_Seguimiento_Solicitud",
                column: "SolicitudEstudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Seguimiento_Solicitud_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "Relacion_Seguimiento_Solicitud",
                column: "SolicitudEstudioId",
                principalTable: "Relacion_Solicitud_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Seguimiento_Solicitud_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "Relacion_Seguimiento_Solicitud");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Seguimiento_Solicitud_SolicitudEstudioId",
                table: "Relacion_Seguimiento_Solicitud");

            migrationBuilder.DropColumn(
                name: "SolicitudEstudioId",
                table: "Relacion_Seguimiento_Solicitud");
        }
    }
}
