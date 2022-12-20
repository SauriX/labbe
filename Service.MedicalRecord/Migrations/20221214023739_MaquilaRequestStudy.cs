using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class MaquilaRequestStudy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaquilaId",
                table: "Relacion_Solicitud_Estudio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CAT_Maquila",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Maquila", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Estudio_MaquilaId",
                table: "Relacion_Solicitud_Estudio",
                column: "MaquilaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_CAT_Maquila_MaquilaId",
                table: "Relacion_Solicitud_Estudio",
                column: "MaquilaId",
                principalTable: "CAT_Maquila",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Estudio_CAT_Maquila_MaquilaId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropTable(
                name: "CAT_Maquila");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Solicitud_Estudio_MaquilaId",
                table: "Relacion_Solicitud_Estudio");

            migrationBuilder.DropColumn(
                name: "MaquilaId",
                table: "Relacion_Solicitud_Estudio");
        }
    }
}
