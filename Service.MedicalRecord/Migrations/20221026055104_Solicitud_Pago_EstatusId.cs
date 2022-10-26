using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class Solicitud_Pago_EstatusId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "EstatusId",
                table: "Relacion_Solicitud_Pago",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "Estatus_Solicitud_Pago",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estatus_Solicitud_Pago", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_Solicitud_Pago_EstatusId",
                table: "Relacion_Solicitud_Pago",
                column: "EstatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacion_Solicitud_Pago_Estatus_Solicitud_Pago_EstatusId",
                table: "Relacion_Solicitud_Pago",
                column: "EstatusId",
                principalTable: "Estatus_Solicitud_Pago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacion_Solicitud_Pago_Estatus_Solicitud_Pago_EstatusId",
                table: "Relacion_Solicitud_Pago");

            migrationBuilder.DropTable(
                name: "Estatus_Solicitud_Pago");

            migrationBuilder.DropIndex(
                name: "IX_Relacion_Solicitud_Pago_EstatusId",
                table: "Relacion_Solicitud_Pago");

            migrationBuilder.DropColumn(
                name: "EstatusId",
                table: "Relacion_Solicitud_Pago");
        }
    }
}
