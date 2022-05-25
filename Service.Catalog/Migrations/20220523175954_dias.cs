using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class dias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Domingo",
                table: "Relacion_Promocion_Paquete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Jueves",
                table: "Relacion_Promocion_Paquete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Lunes",
                table: "Relacion_Promocion_Paquete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Martes",
                table: "Relacion_Promocion_Paquete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Miercoles",
                table: "Relacion_Promocion_Paquete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sabado",
                table: "Relacion_Promocion_Paquete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Viernes",
                table: "Relacion_Promocion_Paquete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Domingo",
                table: "Relacion_Promocion_Estudio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Jueves",
                table: "Relacion_Promocion_Estudio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Lunes",
                table: "Relacion_Promocion_Estudio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Martes",
                table: "Relacion_Promocion_Estudio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Miercoles",
                table: "Relacion_Promocion_Estudio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sabado",
                table: "Relacion_Promocion_Estudio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Viernes",
                table: "Relacion_Promocion_Estudio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Domingo",
                table: "CAT_Promocion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Jueves",
                table: "CAT_Promocion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Lunes",
                table: "CAT_Promocion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Martes",
                table: "CAT_Promocion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Miercoles",
                table: "CAT_Promocion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sabado",
                table: "CAT_Promocion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Viernes",
                table: "CAT_Promocion",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Domingo",
                table: "Relacion_Promocion_Paquete");

            migrationBuilder.DropColumn(
                name: "Jueves",
                table: "Relacion_Promocion_Paquete");

            migrationBuilder.DropColumn(
                name: "Lunes",
                table: "Relacion_Promocion_Paquete");

            migrationBuilder.DropColumn(
                name: "Martes",
                table: "Relacion_Promocion_Paquete");

            migrationBuilder.DropColumn(
                name: "Miercoles",
                table: "Relacion_Promocion_Paquete");

            migrationBuilder.DropColumn(
                name: "Sabado",
                table: "Relacion_Promocion_Paquete");

            migrationBuilder.DropColumn(
                name: "Viernes",
                table: "Relacion_Promocion_Paquete");

            migrationBuilder.DropColumn(
                name: "Domingo",
                table: "Relacion_Promocion_Estudio");

            migrationBuilder.DropColumn(
                name: "Jueves",
                table: "Relacion_Promocion_Estudio");

            migrationBuilder.DropColumn(
                name: "Lunes",
                table: "Relacion_Promocion_Estudio");

            migrationBuilder.DropColumn(
                name: "Martes",
                table: "Relacion_Promocion_Estudio");

            migrationBuilder.DropColumn(
                name: "Miercoles",
                table: "Relacion_Promocion_Estudio");

            migrationBuilder.DropColumn(
                name: "Sabado",
                table: "Relacion_Promocion_Estudio");

            migrationBuilder.DropColumn(
                name: "Viernes",
                table: "Relacion_Promocion_Estudio");

            migrationBuilder.DropColumn(
                name: "Domingo",
                table: "CAT_Promocion");

            migrationBuilder.DropColumn(
                name: "Jueves",
                table: "CAT_Promocion");

            migrationBuilder.DropColumn(
                name: "Lunes",
                table: "CAT_Promocion");

            migrationBuilder.DropColumn(
                name: "Martes",
                table: "CAT_Promocion");

            migrationBuilder.DropColumn(
                name: "Miercoles",
                table: "CAT_Promocion");

            migrationBuilder.DropColumn(
                name: "Sabado",
                table: "CAT_Promocion");

            migrationBuilder.DropColumn(
                name: "Viernes",
                table: "CAT_Promocion");
        }
    }
}
