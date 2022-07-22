using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Sender.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Email",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Remitente = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Smtp = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    RequiereContraseña = table.Column<bool>(type: "bit", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Email", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Email");
        }
    }
}
