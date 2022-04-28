using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class ListaDePrecioActualizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_CompañiaContacto_CAT_Compañia_CompañiaId",
                table: "CAT_CompañiaContacto");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_ListaP_Compañia_CAT_Compañia_CompanyId",
                table: "CAT_ListaP_Compañia");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_ListaP_Compañia_CAT_ListPrecio_PrecioId",
                table: "CAT_ListaP_Compañia");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_ListaP_Promocion_CAT_ListPrecio_PrecioId",
                table: "CAT_ListaP_Promocion");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_ListaP_Promocion_CAT_ListPrecio_PriceId",
                table: "CAT_ListaP_Promocion");

            migrationBuilder.DropIndex(
                name: "IX_CAT_ListaP_Compañia_CompanyId",
                table: "CAT_ListaP_Compañia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_ListPrecio",
                table: "CAT_ListPrecio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_CompañiaContacto",
                table: "CAT_CompañiaContacto");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CAT_ListaP_Compañia");

            migrationBuilder.RenameTable(
                name: "CAT_ListPrecio",
                newName: "CAT_ListaPrecio");

            migrationBuilder.RenameTable(
                name: "CAT_CompañiaContacto",
                newName: "Contact");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_CompañiaContacto_CompañiaId",
                table: "Contact",
                newName: "IX_Contact_CompañiaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_ListaPrecio",
                table: "CAT_ListaPrecio",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                table: "Contact",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaP_Compañia_CompañiaId",
                table: "CAT_ListaP_Compañia",
                column: "CompañiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_ListaP_Compañia_CAT_Compañia_CompañiaId",
                table: "CAT_ListaP_Compañia",
                column: "CompañiaId",
                principalTable: "CAT_Compañia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_ListaP_Compañia_CAT_ListaPrecio_PrecioId",
                table: "CAT_ListaP_Compañia",
                column: "PrecioId",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_ListaP_Promocion_CAT_ListaPrecio_PrecioId",
                table: "CAT_ListaP_Promocion",
                column: "PrecioId",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_ListaP_Promocion_CAT_ListaPrecio_PriceId",
                table: "CAT_ListaP_Promocion",
                column: "PriceId",
                principalTable: "CAT_ListaPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_CAT_Compañia_CompañiaId",
                table: "Contact",
                column: "CompañiaId",
                principalTable: "CAT_Compañia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_ListaP_Compañia_CAT_Compañia_CompañiaId",
                table: "CAT_ListaP_Compañia");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_ListaP_Compañia_CAT_ListaPrecio_PrecioId",
                table: "CAT_ListaP_Compañia");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_ListaP_Promocion_CAT_ListaPrecio_PrecioId",
                table: "CAT_ListaP_Promocion");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_ListaP_Promocion_CAT_ListaPrecio_PriceId",
                table: "CAT_ListaP_Promocion");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_CAT_Compañia_CompañiaId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_CAT_ListaP_Compañia_CompañiaId",
                table: "CAT_ListaP_Compañia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                table: "Contact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CAT_ListaPrecio",
                table: "CAT_ListaPrecio");

            migrationBuilder.RenameTable(
                name: "Contact",
                newName: "CAT_CompañiaContacto");

            migrationBuilder.RenameTable(
                name: "CAT_ListaPrecio",
                newName: "CAT_ListPrecio");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_CompañiaId",
                table: "CAT_CompañiaContacto",
                newName: "IX_CAT_CompañiaContacto_CompañiaId");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "CAT_ListaP_Compañia",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_CompañiaContacto",
                table: "CAT_CompañiaContacto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAT_ListPrecio",
                table: "CAT_ListPrecio",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_ListaP_Compañia_CompanyId",
                table: "CAT_ListaP_Compañia",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_CompañiaContacto_CAT_Compañia_CompañiaId",
                table: "CAT_CompañiaContacto",
                column: "CompañiaId",
                principalTable: "CAT_Compañia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_ListaP_Compañia_CAT_Compañia_CompanyId",
                table: "CAT_ListaP_Compañia",
                column: "CompanyId",
                principalTable: "CAT_Compañia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_ListaP_Compañia_CAT_ListPrecio_PrecioId",
                table: "CAT_ListaP_Compañia",
                column: "PrecioId",
                principalTable: "CAT_ListPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_ListaP_Promocion_CAT_ListPrecio_PrecioId",
                table: "CAT_ListaP_Promocion",
                column: "PrecioId",
                principalTable: "CAT_ListPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_ListaP_Promocion_CAT_ListPrecio_PriceId",
                table: "CAT_ListaP_Promocion",
                column: "PriceId",
                principalTable: "CAT_ListPrecio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
