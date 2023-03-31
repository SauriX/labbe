using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Sender.Migrations
{
    public partial class historiOfNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Para = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    EsAlerta = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Notifications", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Notifications");
        }
    }
}
