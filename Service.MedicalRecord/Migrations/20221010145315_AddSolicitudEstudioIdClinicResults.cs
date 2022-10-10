using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class AddSolicitudEstudioIdClinicResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicResults_Relacion_Solicitud_Estudio_EstudioId",
                table: "ClinicResults");

            migrationBuilder.DropIndex(
                name: "IX_ClinicResults_EstudioId",
                table: "ClinicResults");

            migrationBuilder.AddColumn<int>(
                name: "SolicitudEstudioId",
                table: "ClinicResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClinicResults_SolicitudEstudioId",
                table: "ClinicResults",
                column: "SolicitudEstudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicResults_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "ClinicResults",
                column: "SolicitudEstudioId",
                principalTable: "Relacion_Solicitud_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicResults_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "ClinicResults");

            migrationBuilder.DropIndex(
                name: "IX_ClinicResults_SolicitudEstudioId",
                table: "ClinicResults");

            migrationBuilder.DropColumn(
                name: "SolicitudEstudioId",
                table: "ClinicResults");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicResults_EstudioId",
                table: "ClinicResults",
                column: "EstudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicResults_Relacion_Solicitud_Estudio_EstudioId",
                table: "ClinicResults",
                column: "EstudioId",
                principalTable: "Relacion_Solicitud_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
