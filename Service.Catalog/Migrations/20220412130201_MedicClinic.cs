using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class MedicClinic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Medico_Clinica_CAT_Medico_MedicoId",
                table: "CAT_Medico_Clinica");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Medico_Clinica_CAT_Medico_MedicsIdMedico",
                table: "CAT_Medico_Clinica");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Medico_Clinica_MedicsIdMedico",
                table: "CAT_Medico_Clinica");

            migrationBuilder.DropColumn(
                name: "MedicsIdMedico",
                table: "CAT_Medico_Clinica");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Medico_Clinica_CAT_Medico_MedicoId",
                table: "CAT_Medico_Clinica",
                column: "MedicoId",
                principalTable: "CAT_Medico",
                principalColumn: "IdMedico",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Medico_Clinica_CAT_Medico_MedicoId",
                table: "CAT_Medico_Clinica");

            migrationBuilder.AddColumn<int>(
                name: "MedicsIdMedico",
                table: "CAT_Medico_Clinica",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Medico_Clinica_MedicsIdMedico",
                table: "CAT_Medico_Clinica",
                column: "MedicsIdMedico");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Medico_Clinica_CAT_Medico_MedicoId",
                table: "CAT_Medico_Clinica",
                column: "MedicoId",
                principalTable: "CAT_Medico",
                principalColumn: "IdMedico",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Medico_Clinica_CAT_Medico_MedicsIdMedico",
                table: "CAT_Medico_Clinica",
                column: "MedicsIdMedico",
                principalTable: "CAT_Medico",
                principalColumn: "IdMedico",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
