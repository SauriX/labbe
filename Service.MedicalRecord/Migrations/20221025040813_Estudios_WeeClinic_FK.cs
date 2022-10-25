using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class Estudios_WeeClinic_FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstudioWeeClinicId",
                table: "Relacion_Solicitud_Estudio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Relacion_Estudio_WeeClinic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudEstudioId = table.Column<int>(type: "int", nullable: false),
                    IdOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdServicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cubierto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<int>(type: "int", nullable: false),
                    RestanteDays = table.Column<int>(type: "int", nullable: false),
                    Vigencia = table.Column<int>(type: "int", nullable: false),
                    IsCancel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_Estudio_WeeClinic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacion_Estudio_WeeClinic_Relacion_Solicitud_Estudio_SolicitudEstudioId",
                        column: x => x.SolicitudEstudioId,
                        principalTable: "Relacion_Solicitud_Estudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Estudio_WeeClinic_SolicitudEstudioId",
                table: "Relacion_Estudio_WeeClinic",
                column: "SolicitudEstudioId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_Estudio_WeeClinic");

            migrationBuilder.DropColumn(
                name: "EstudioWeeClinicId",
                table: "Relacion_Solicitud_Estudio");
        }
    }
}
