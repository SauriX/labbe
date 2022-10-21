using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class RelacionEstudiosconResultados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cat_Captura_ResultadosPatologicos_Relacion_Solicitud_Estudio_EstudioId",
                table: "Cat_Captura_ResultadosPatologicos");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicResults_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "ClinicResults");

            migrationBuilder.RenameColumn(
                name: "EstudioId",
                table: "Cat_Captura_ResultadosPatologicos",
                newName: "SolicitudEstudioId");

            migrationBuilder.RenameIndex(
                name: "IX_Cat_Captura_ResultadosPatologicos_EstudioId",
                table: "Cat_Captura_ResultadosPatologicos",
                newName: "IX_Cat_Captura_ResultadosPatologicos_SolicitudEstudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cat_Captura_ResultadosPatologicos_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "Cat_Captura_ResultadosPatologicos",
                column: "SolicitudEstudioId",
                principalTable: "Relacion_Solicitud_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicResults_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "ClinicResults",
                column: "SolicitudEstudioId",
                principalTable: "Relacion_Solicitud_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cat_Captura_ResultadosPatologicos_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "Cat_Captura_ResultadosPatologicos");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicResults_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "ClinicResults");

            migrationBuilder.RenameColumn(
                name: "SolicitudEstudioId",
                table: "Cat_Captura_ResultadosPatologicos",
                newName: "EstudioId");

            migrationBuilder.RenameIndex(
                name: "IX_Cat_Captura_ResultadosPatologicos_SolicitudEstudioId",
                table: "Cat_Captura_ResultadosPatologicos",
                newName: "IX_Cat_Captura_ResultadosPatologicos_EstudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cat_Captura_ResultadosPatologicos_Relacion_Solicitud_Estudio_EstudioId",
                table: "Cat_Captura_ResultadosPatologicos",
                column: "EstudioId",
                principalTable: "Relacion_Solicitud_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicResults_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "ClinicResults",
                column: "SolicitudEstudioId",
                principalTable: "Relacion_Solicitud_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
