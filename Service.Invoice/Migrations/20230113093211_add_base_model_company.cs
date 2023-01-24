using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Billing.Migrations
{
    public partial class add_base_model_company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
           

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreo",
                table: "Relacion_Factura_Solicitudes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModifico",
                table: "Relacion_Factura_Solicitudes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCreoId",
                table: "Relacion_Factura_Solicitudes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreo",
                table: "CAT_Factura_Companias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModifico",
                table: "CAT_Factura_Companias",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCreoId",
                table: "CAT_Factura_Companias",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioModificoId",
                table: "CAT_Factura_Companias",
                type: "uniqueidentifier",
                nullable: true);

           
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "FechaCreo",
                table: "Relacion_Factura_Solicitudes");

            migrationBuilder.DropColumn(
                name: "FechaModifico",
                table: "Relacion_Factura_Solicitudes");

            migrationBuilder.DropColumn(
                name: "UsuarioCreoId",
                table: "Relacion_Factura_Solicitudes");

            migrationBuilder.DropColumn(
                name: "FechaCreo",
                table: "CAT_Factura_Companias");

            migrationBuilder.DropColumn(
                name: "FechaModifico",
                table: "CAT_Factura_Companias");

            migrationBuilder.DropColumn(
                name: "UsuarioCreoId",
                table: "CAT_Factura_Companias");

            migrationBuilder.DropColumn(
                name: "UsuarioModificoId",
                table: "CAT_Factura_Companias");

            
        }
    }
}
