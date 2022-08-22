using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Report.Migrations
{
    public partial class TablaMaquilayrelacionconestudios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parcialidad",
                table: "RequestStudy");

            migrationBuilder.AddColumn<Guid>(
                name: "MaquilaId",
                table: "RequestStudy",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SucursalId",
                table: "RequestStudy",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Maquila",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquila", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestStudy_MaquilaId",
                table: "RequestStudy",
                column: "MaquilaId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStudy_SucursalId",
                table: "RequestStudy",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStudy_Branch_SucursalId",
                table: "RequestStudy",
                column: "SucursalId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStudy_Maquila_MaquilaId",
                table: "RequestStudy",
                column: "MaquilaId",
                principalTable: "Maquila",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestStudy_Branch_SucursalId",
                table: "RequestStudy");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestStudy_Maquila_MaquilaId",
                table: "RequestStudy");

            migrationBuilder.DropTable(
                name: "Maquila");

            migrationBuilder.DropIndex(
                name: "IX_RequestStudy_MaquilaId",
                table: "RequestStudy");

            migrationBuilder.DropIndex(
                name: "IX_RequestStudy_SucursalId",
                table: "RequestStudy");

            migrationBuilder.DropColumn(
                name: "MaquilaId",
                table: "RequestStudy");

            migrationBuilder.DropColumn(
                name: "SucursalId",
                table: "RequestStudy");

            migrationBuilder.AddColumn<byte>(
                name: "Parcialidad",
                table: "RequestStudy",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
