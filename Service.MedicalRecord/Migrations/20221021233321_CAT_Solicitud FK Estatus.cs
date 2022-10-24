using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class CAT_SolicitudFKEstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CAT_Solicitud_EstatusId",
                table: "CAT_Solicitud",
                column: "EstatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Solicitud_Estatus_Solicitud_EstatusId",
                table: "CAT_Solicitud",
                column: "EstatusId",
                principalTable: "Estatus_Solicitud",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Solicitud_Estatus_Solicitud_EstatusId",
                table: "CAT_Solicitud");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Solicitud_EstatusId",
                table: "CAT_Solicitud");
        }
    }
}
