using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Report.Migrations
{
    public partial class ModificarconfigurationRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestStudy_Request_SolicitudId",
                table: "RequestStudy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Request",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Request");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Request",
                table: "Request",
                column: "SolicitudId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStudy_Request_SolicitudId",
                table: "RequestStudy",
                column: "SolicitudId",
                principalTable: "Request",
                principalColumn: "SolicitudId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestStudy_Request_SolicitudId",
                table: "RequestStudy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Request",
                table: "Request");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Request",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Request",
                table: "Request",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStudy_Request_SolicitudId",
                table: "RequestStudy",
                column: "SolicitudId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
