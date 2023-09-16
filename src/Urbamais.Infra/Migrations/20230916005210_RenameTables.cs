using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urbamais.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_registrofinanceiro_centrocusto_centro_custo_id",
                table: "registrofinanceiro");

            migrationBuilder.DropForeignKey(
                name: "FK_registrofinanceiro_centrocusto_classe_financeira_id",
                table: "registrofinanceiro");

            migrationBuilder.DropForeignKey(
                name: "FK_registrofinanceiro_obra_obra_id",
                table: "registrofinanceiro");

            migrationBuilder.RenameTable(
                name: "registrofinanceiro",
                newName: "registro_financeiro");

            migrationBuilder.RenameTable(
                name: "centrocusto",
                newName: "centro_custo");

            migrationBuilder.RenameIndex(
                name: "IX_registrofinanceiro_obra_id",
                table: "registro_financeiro",
                newName: "IX_registro_financeiro_obra_id");

            migrationBuilder.RenameIndex(
                name: "IX_registrofinanceiro_classe_financeira_id",
                table: "registro_financeiro",
                newName: "IX_registro_financeiro_classe_financeira_id");

            migrationBuilder.RenameIndex(
                name: "IX_registrofinanceiro_centro_custo_id",
                table: "registro_financeiro",
                newName: "IX_registro_financeiro_centro_custo_id");

            migrationBuilder.AddForeignKey(
                name: "FK_registro_financeiro_centro_custo_centro_custo_id",
                table: "registro_financeiro",
                column: "centro_custo_id",
                principalTable: "centro_custo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_registro_financeiro_centro_custo_classe_financeira_id",
                table: "registro_financeiro",
                column: "classe_financeira_id",
                principalTable: "centro_custo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_registro_financeiro_obra_obra_id",
                table: "registro_financeiro",
                column: "obra_id",
                principalTable: "obra",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_registro_financeiro_centro_custo_centro_custo_id",
                table: "registro_financeiro");

            migrationBuilder.DropForeignKey(
                name: "FK_registro_financeiro_centro_custo_classe_financeira_id",
                table: "registro_financeiro");

            migrationBuilder.DropForeignKey(
                name: "FK_registro_financeiro_obra_obra_id",
                table: "registro_financeiro");

            migrationBuilder.RenameTable(
                name: "registro_financeiro",
                newName: "registrofinanceiro");

            migrationBuilder.RenameTable(
                name: "centro_custo",
                newName: "centrocusto");

            migrationBuilder.RenameIndex(
                name: "IX_registro_financeiro_obra_id",
                table: "registrofinanceiro",
                newName: "IX_registrofinanceiro_obra_id");

            migrationBuilder.RenameIndex(
                name: "IX_registro_financeiro_classe_financeira_id",
                table: "registrofinanceiro",
                newName: "IX_registrofinanceiro_classe_financeira_id");

            migrationBuilder.RenameIndex(
                name: "IX_registro_financeiro_centro_custo_id",
                table: "registrofinanceiro",
                newName: "IX_registrofinanceiro_centro_custo_id");

            migrationBuilder.AddForeignKey(
                name: "FK_registrofinanceiro_centrocusto_centro_custo_id",
                table: "registrofinanceiro",
                column: "centro_custo_id",
                principalTable: "centrocusto",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_registrofinanceiro_centrocusto_classe_financeira_id",
                table: "registrofinanceiro",
                column: "classe_financeira_id",
                principalTable: "centrocusto",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_registrofinanceiro_obra_obra_id",
                table: "registrofinanceiro",
                column: "obra_id",
                principalTable: "obra",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
