using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Report.Migrations
{
    public partial class TablasRequestRequestStudyyRequestStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duracion",
                table: "RequestStudy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "Parcialidad",
                table: "RequestStudy",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<bool>(
                name: "Parcialidad",
                table: "Request",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "MedicalRecord",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "MedicalRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestStudy_EstatusId",
                table: "RequestStudy",
                column: "EstatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStudy_RequestStatus_EstatusId",
                table: "RequestStudy",
                column: "EstatusId",
                principalTable: "RequestStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestStudy_RequestStatus_EstatusId",
                table: "RequestStudy");

            migrationBuilder.DropIndex(
                name: "IX_RequestStudy_EstatusId",
                table: "RequestStudy");

            migrationBuilder.DropColumn(
                name: "Duracion",
                table: "RequestStudy");

            migrationBuilder.DropColumn(
                name: "Parcialidad",
                table: "RequestStudy");

            migrationBuilder.DropColumn(
                name: "Parcialidad",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Edad",
                table: "MedicalRecord");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "MedicalRecord");
        }
    }
}
