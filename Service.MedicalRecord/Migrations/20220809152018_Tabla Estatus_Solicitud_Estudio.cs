using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class TablaEstatus_Solicitud_Estudio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estatus_Solicitud_Estudio",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estatus_Solicitud_Estudio", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Estudio_EstatusId",
                table: "Relacion_Solicitud_Estudio",
                column: "EstatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_Estatus_Solicitud_Estudio_EstatusId",
                table: "Relacion_Solicitud_Estudio",
                column: "EstatusId",
                principalTable: "Estatus_Solicitud_Estudio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_Estatus_Solicitud_Estudio_EstatusId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropTable(
                name: "Estatus_Solicitud_Estudio");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Solicitud_Estudio_EstatusId",
                table: "Relacion_Solicitud_Estudio");
        }
    }
}
