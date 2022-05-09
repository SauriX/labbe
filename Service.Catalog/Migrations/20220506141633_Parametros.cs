using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Catalog.Migrations
{
    public partial class Parametros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_CAT_Departamento_DepartmentId",
                table: "CAT_Parametro");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_Cat_Formato_FormatId",
                table: "CAT_Parametro");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_CAT_Reactivo_Contpaq_ReagentId",
                table: "CAT_Parametro");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Tipo_Valor_CAT_Parametro_IdParametro",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropColumn(
                name: "MedidaTiempo",
                table: "CAT_Tipo_Valor");

            migrationBuilder.RenameColumn(
                name: "UsuarioModId",
                table: "CAT_Tipo_Valor",
                newName: "UsuarioModificoId");

            migrationBuilder.RenameColumn(
                name: "IdParametro",
                table: "CAT_Tipo_Valor",
                newName: "ParametroId");

            migrationBuilder.RenameColumn(
                name: "FechaMod",
                table: "CAT_Tipo_Valor",
                newName: "FechaModifico");

            migrationBuilder.RenameColumn(
                name: "IdTipo_Valor",
                table: "CAT_Tipo_Valor",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Tipo_Valor_IdParametro",
                table: "CAT_Tipo_Valor",
                newName: "IX_CAT_Tipo_Valor_ParametroId");

            migrationBuilder.RenameColumn(
                name: "UsuarioModId",
                table: "CAT_Parametro",
                newName: "UsuarioModificoId");

            migrationBuilder.RenameColumn(
                name: "ReagentId",
                table: "CAT_Parametro",
                newName: "ReactivoId");

            migrationBuilder.RenameColumn(
                name: "FormatId",
                table: "CAT_Parametro",
                newName: "FormatoImpresionId");

            migrationBuilder.RenameColumn(
                name: "FechaMod",
                table: "CAT_Parametro",
                newName: "FechaModifico");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "CAT_Parametro",
                newName: "DepartmentoId");

            migrationBuilder.RenameColumn(
                name: "DepartamentId",
                table: "CAT_Parametro",
                newName: "DepartamentoId");

            migrationBuilder.RenameColumn(
                name: "IdParametro",
                table: "CAT_Parametro",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Parametro_ReagentId",
                table: "CAT_Parametro",
                newName: "IX_CAT_Parametro_ReactivoId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Parametro_FormatId",
                table: "CAT_Parametro",
                newName: "IX_CAT_Parametro_FormatoImpresionId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Parametro_DepartmentId",
                table: "CAT_Parametro",
                newName: "IX_CAT_Parametro_DepartmentoId");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorInicialNumerico",
                table: "CAT_Tipo_Valor",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorInicial",
                table: "CAT_Tipo_Valor",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorFinalNumerico",
                table: "CAT_Tipo_Valor",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorFinal",
                table: "CAT_Tipo_Valor",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RangoEdadInicial",
                table: "CAT_Tipo_Valor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RangoEdadFinal",
                table: "CAT_Tipo_Valor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MujerValorInicial",
                table: "CAT_Tipo_Valor",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MujerValorFinal",
                table: "CAT_Tipo_Valor",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "HombreValorInicial",
                table: "CAT_Tipo_Valor",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "HombreValorFinal",
                table: "CAT_Tipo_Valor",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "MedidaTiempoId",
                table: "CAT_Tipo_Valor",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorInicial",
                table: "CAT_Parametro",
                type: "decimal(18,2)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "Unidades",
                table: "CAT_Parametro",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "TipoValor",
                table: "CAT_Parametro",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "0",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100,
                oldDefaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_CAT_Departamento_DepartmentoId",
                table: "CAT_Parametro",
                column: "DepartmentoId",
                principalTable: "CAT_Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_Cat_Formato_FormatoImpresionId",
                table: "CAT_Parametro",
                column: "FormatoImpresionId",
                principalTable: "Cat_Formato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_CAT_Reactivo_Contpaq_ReactivoId",
                table: "CAT_Parametro",
                column: "ReactivoId",
                principalTable: "CAT_Reactivo_Contpaq",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Tipo_Valor_CAT_Parametro_ParametroId",
                table: "CAT_Tipo_Valor",
                column: "ParametroId",
                principalTable: "CAT_Parametro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_CAT_Departamento_DepartmentoId",
                table: "CAT_Parametro");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_Cat_Formato_FormatoImpresionId",
                table: "CAT_Parametro");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Parametro_CAT_Reactivo_Contpaq_ReactivoId",
                table: "CAT_Parametro");

            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Tipo_Valor_CAT_Parametro_ParametroId",
                table: "CAT_Tipo_Valor");

            migrationBuilder.DropColumn(
                name: "MedidaTiempoId",
                table: "CAT_Tipo_Valor");

            migrationBuilder.RenameColumn(
                name: "UsuarioModificoId",
                table: "CAT_Tipo_Valor",
                newName: "UsuarioModId");

            migrationBuilder.RenameColumn(
                name: "ParametroId",
                table: "CAT_Tipo_Valor",
                newName: "IdParametro");

            migrationBuilder.RenameColumn(
                name: "FechaModifico",
                table: "CAT_Tipo_Valor",
                newName: "FechaMod");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CAT_Tipo_Valor",
                newName: "IdTipo_Valor");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Tipo_Valor_ParametroId",
                table: "CAT_Tipo_Valor",
                newName: "IX_CAT_Tipo_Valor_IdParametro");

            migrationBuilder.RenameColumn(
                name: "UsuarioModificoId",
                table: "CAT_Parametro",
                newName: "UsuarioModId");

            migrationBuilder.RenameColumn(
                name: "ReactivoId",
                table: "CAT_Parametro",
                newName: "ReagentId");

            migrationBuilder.RenameColumn(
                name: "FormatoImpresionId",
                table: "CAT_Parametro",
                newName: "FormatId");

            migrationBuilder.RenameColumn(
                name: "FechaModifico",
                table: "CAT_Parametro",
                newName: "FechaMod");

            migrationBuilder.RenameColumn(
                name: "DepartmentoId",
                table: "CAT_Parametro",
                newName: "DepartmentId");

            migrationBuilder.RenameColumn(
                name: "DepartamentoId",
                table: "CAT_Parametro",
                newName: "DepartamentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CAT_Parametro",
                newName: "IdParametro");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Parametro_ReactivoId",
                table: "CAT_Parametro",
                newName: "IX_CAT_Parametro_ReagentId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Parametro_FormatoImpresionId",
                table: "CAT_Parametro",
                newName: "IX_CAT_Parametro_FormatId");

            migrationBuilder.RenameIndex(
                name: "IX_CAT_Parametro_DepartmentoId",
                table: "CAT_Parametro",
                newName: "IX_CAT_Parametro_DepartmentId");

            migrationBuilder.AlterColumn<string>(
                name: "ValorInicialNumerico",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ValorInicial",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ValorFinalNumerico",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ValorFinal",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "RangoEdadInicial",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RangoEdadFinal",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MujerValorInicial",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "MujerValorFinal",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "HombreValorInicial",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "HombreValorFinal",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "MedidaTiempo",
                table: "CAT_Tipo_Valor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ValorInicial",
                table: "CAT_Parametro",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<double>(
                name: "Unidades",
                table: "CAT_Parametro",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "TipoValor",
                table: "CAT_Parametro",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "0");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_CAT_Departamento_DepartmentId",
                table: "CAT_Parametro",
                column: "DepartmentId",
                principalTable: "CAT_Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_Cat_Formato_FormatId",
                table: "CAT_Parametro",
                column: "FormatId",
                principalTable: "Cat_Formato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Parametro_CAT_Reactivo_Contpaq_ReagentId",
                table: "CAT_Parametro",
                column: "ReagentId",
                principalTable: "CAT_Reactivo_Contpaq",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Tipo_Valor_CAT_Parametro_IdParametro",
                table: "CAT_Tipo_Valor",
                column: "IdParametro",
                principalTable: "CAT_Parametro",
                principalColumn: "IdParametro",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
