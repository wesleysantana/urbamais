using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Urbamais.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Financeiro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_obra_empresa_empresa_id",
                table: "obra");

            migrationBuilder.RenameColumn(
                name: "empresa_id",
                table: "obra",
                newName: "EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_obra_empresa_id",
                table: "obra",
                newName: "IX_obra_EmpresaId");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "obra",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "ordem_servico",
                table: "colaborador",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ficha_registro",
                table: "colaborador",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "exame_admissional",
                table: "colaborador",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "epi",
                table: "colaborador",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "cnh",
                table: "colaborador",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "centrocusto",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    reduzido = table.Column<int>(type: "integer", nullable: false),
                    descricao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    natureza = table.Column<int>(type: "integer", nullable: false),
                    extenso = table.Column<long>(type: "bigint", nullable: false),
                    id_user_creation = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "text", nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "text", nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("centrocusto_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "registrofinanceiro",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    obra_id = table.Column<int>(type: "integer", nullable: false),
                    fornecedor_id = table.Column<int>(type: "integer", nullable: false),
                    fornecedor = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    data_emissao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_vencimento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_entrada = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    tipo_doc = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    numero_doc = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    parcela = table.Column<int>(type: "integer", nullable: false),
                    aprovacao_pagamento = table.Column<int>(type: "integer", nullable: false),
                    valor = table.Column<decimal>(type: "numeric", nullable: false),
                    caucao = table.Column<decimal>(type: "numeric", nullable: false),
                    total = table.Column<decimal>(type: "numeric", nullable: false),
                    desconto = table.Column<decimal>(type: "numeric", nullable: false),
                    valor_acrescimo = table.Column<decimal>(type: "numeric", nullable: false),
                    valor_liquido = table.Column<decimal>(type: "numeric", nullable: false),
                    valor_baixa = table.Column<decimal>(type: "numeric", nullable: false),
                    complemento = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    centro_custo_id = table.Column<int>(type: "integer", nullable: false),
                    classe_financeira_id = table.Column<int>(type: "integer", nullable: false),
                    id_user_creation = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "text", nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "text", nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("registrofinanceiro_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_registrofinanceiro_centrocusto_centro_custo_id",
                        column: x => x.centro_custo_id,
                        principalTable: "centrocusto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_registrofinanceiro_centrocusto_classe_financeira_id",
                        column: x => x.classe_financeira_id,
                        principalTable: "centrocusto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_registrofinanceiro_obra_obra_id",
                        column: x => x.obra_id,
                        principalTable: "obra",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_registrofinanceiro_centro_custo_id",
                table: "registrofinanceiro",
                column: "centro_custo_id");

            migrationBuilder.CreateIndex(
                name: "IX_registrofinanceiro_classe_financeira_id",
                table: "registrofinanceiro",
                column: "classe_financeira_id");

            migrationBuilder.CreateIndex(
                name: "IX_registrofinanceiro_obra_id",
                table: "registrofinanceiro",
                column: "obra_id");

            migrationBuilder.AddForeignKey(
                name: "FK_obra_empresa_EmpresaId",
                table: "obra",
                column: "EmpresaId",
                principalTable: "empresa",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_obra_empresa_EmpresaId",
                table: "obra");

            migrationBuilder.DropTable(
                name: "registrofinanceiro");

            migrationBuilder.DropTable(
                name: "centrocusto");

            migrationBuilder.RenameColumn(
                name: "EmpresaId",
                table: "obra",
                newName: "empresa_id");

            migrationBuilder.RenameIndex(
                name: "IX_obra_EmpresaId",
                table: "obra",
                newName: "IX_obra_empresa_id");

            migrationBuilder.AlterColumn<int>(
                name: "empresa_id",
                table: "obra",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ordem_servico",
                table: "colaborador",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ficha_registro",
                table: "colaborador",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "exame_admissional",
                table: "colaborador",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "epi",
                table: "colaborador",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "cnh",
                table: "colaborador",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_obra_empresa_empresa_id",
                table: "obra",
                column: "empresa_id",
                principalTable: "empresa",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
