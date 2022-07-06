using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.MedicalRecord.Migrations
{
    public partial class removeestudyid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstudioId",
                table: "cotizacionStudies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EstudioId",
                table: "cotizacionStudies",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
