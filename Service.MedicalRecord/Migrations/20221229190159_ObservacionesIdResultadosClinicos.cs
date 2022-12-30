using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class ObservacionesIdResultadosClinicos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicResults_CAT_Solicitud_SolicitudId",
                table: "ClinicResults");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicResults_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "ClinicResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicResults",
                table: "ClinicResults");

            migrationBuilder.RenameTable(
                name: "ClinicResults",
                newName: "Resultados_Clinicos");

            migrationBuilder.RenameIndex(
                name: "IX_ClinicResults_SolicitudId",
                table: "Resultados_Clinicos",
                newName: "IX_Resultados_Clinicos_SolicitudId");

            migrationBuilder.RenameIndex(
                name: "IX_ClinicResults_SolicitudEstudioId",
                table: "Resultados_Clinicos",
                newName: "IX_Resultados_Clinicos_SolicitudEstudioId");

            migrationBuilder.AddColumn<string>(
                name: "ObservacionesId",
                table: "Resultados_Clinicos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resultados_Clinicos",
                table: "Resultados_Clinicos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resultados_Clinicos_CAT_Solicitud_SolicitudId",
                table: "Resultados_Clinicos",
                column: "SolicitudId",
                principalTable: "CAT_Solicitud",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resultados_Clinicos_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "Resultados_Clinicos",
                column: "SolicitudEstudioId",
                principalTable: "Relacion_Solicitud_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resultados_Clinicos_CAT_Solicitud_SolicitudId",
                table: "Resultados_Clinicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Resultados_Clinicos_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "Resultados_Clinicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resultados_Clinicos",
                table: "Resultados_Clinicos");

            migrationBuilder.DropColumn(
                name: "ObservacionesId",
                table: "Resultados_Clinicos");

            migrationBuilder.RenameTable(
                name: "Resultados_Clinicos",
                newName: "ClinicResults");

            migrationBuilder.RenameIndex(
                name: "IX_Resultados_Clinicos_SolicitudId",
                table: "ClinicResults",
                newName: "IX_ClinicResults_SolicitudId");

            migrationBuilder.RenameIndex(
                name: "IX_Resultados_Clinicos_SolicitudEstudioId",
                table: "ClinicResults",
                newName: "IX_ClinicResults_SolicitudEstudioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicResults",
                table: "ClinicResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicResults_CAT_Solicitud_SolicitudId",
                table: "ClinicResults",
                column: "SolicitudId",
                principalTable: "CAT_Solicitud",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicResults_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                table: "ClinicResults",
                column: "SolicitudEstudioId",
                principalTable: "Relacion_Solicitud_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
