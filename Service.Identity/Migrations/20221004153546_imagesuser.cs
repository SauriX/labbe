using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Identity.Migrations
{
    public partial class imagesuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relacion_User_Imagen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ruta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioCreoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaModifico = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacion_User_Imagen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacion_User_Imagen_CAT_Usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "CAT_Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relacion_User_Imagen_UserId",
                table: "Relacion_User_Imagen",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relacion_User_Imagen");
        }
    }
}
