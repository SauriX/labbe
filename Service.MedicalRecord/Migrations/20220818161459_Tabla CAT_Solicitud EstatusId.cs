using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class TablaCAT_SolicitudEstatusId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "EstatusId",
                table: "CAT_Solicitud",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)1);

            migrationBuilder.CreateTable(
                name: "Estatus_Solicitud",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estatus_Solicitud", x => x.Id);
                });

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

            migrationBuilder.DropTable(
                name: "Estatus_Solicitud");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Solicitud_EstatusId",
                table: "CAT_Solicitud");

            migrationBuilder.DropColumn(
                name: "EstatusId",
                table: "CAT_Solicitud");
        }
    }
}
