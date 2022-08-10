using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class TablaTaponyRelacionSolicitudTapon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaponId",
                table: "Relacion_Solicitud_Estudio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CAT_Tipo_Tapon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Tipo_Tapon", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Estudio_TaponId",
                table: "Relacion_Solicitud_Estudio",
                column: "TaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_CAT_Tipo_Tapon_TaponId",
                table: "Relacion_Solicitud_Estudio",
                column: "TaponId",
                principalTable: "CAT_Tipo_Tapon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_CAT_Tipo_Tapon_TaponId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropTable(
                name: "CAT_Tipo_Tapon");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Solicitud_Estudio_TaponId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "TaponId",
                table: "Relacion_Solicitud_Estudio");
        }
    }
}
