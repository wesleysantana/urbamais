using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Urbamais.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cidade",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    uf = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cidade_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "colaborador",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    numero_carteira_trabalho = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    numero_cnh = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: true),
                    tipo_cnh = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    data_validade_cnh = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    numero_exame_admissional = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    data_validade_exame_admissional = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("colaborador_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "email",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    endereco = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("email_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "companie",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    nome_fantasia = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    razao_social = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    inscricao_estadual = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    inscricao_municipal = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("companie_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipamento",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    descricao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("equipamento_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fornecedor",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    nome_fantasia = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    razao_social = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    inscricao_estadual = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    inscricao_municipal = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("fornecedor_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "telefone",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    numero = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("telefone_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "unidade",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    descricao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("unidade_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "endereco",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    cidade_id = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    logradouro = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    numero = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    complemento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    bairro = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cep = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("endereco_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_endereco_cidade_cidade_id",
                        column: x => x.cidade_id,
                        principalTable: "cidade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "colaboradores_emails",
                columns: table => new
                {
                    colaborador_id = table.Column<int>(type: "integer", nullable: false),
                    email_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colaboradores_emails", x => new { x.colaborador_id, x.email_id });
                    table.ForeignKey(
                        name: "FK_colaboradores_emails_colaborador_colaborador_id",
                        column: x => x.colaborador_id,
                        principalTable: "colaborador",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_colaboradores_emails_email_email_id",
                        column: x => x.email_id,
                        principalTable: "email",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "companies_emails",
                columns: table => new
                {
                    email_id = table.Column<int>(type: "integer", nullable: false),
                    companie_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies_emails", x => new { x.email_id, x.companie_id });
                    table.ForeignKey(
                        name: "FK_companies_emails_email_email_id",
                        column: x => x.email_id,
                        principalTable: "email",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_companies_emails_companie_companie_id",
                        column: x => x.companie_id,
                        principalTable: "companie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "obra",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    companie_id = table.Column<int>(type: "integer", nullable: false),
                    descricao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("obra_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_obra_companie_companie_id",
                        column: x => x.companie_id,
                        principalTable: "companie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fornecedores_emails",
                columns: table => new
                {
                    email_id = table.Column<int>(type: "integer", nullable: false),
                    fornecedor_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedores_emails", x => new { x.email_id, x.fornecedor_id });
                    table.ForeignKey(
                        name: "FK_fornecedores_emails_email_email_id",
                        column: x => x.email_id,
                        principalTable: "email",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fornecedores_emails_fornecedor_fornecedor_id",
                        column: x => x.fornecedor_id,
                        principalTable: "fornecedor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fornecedores_equipamentos",
                columns: table => new
                {
                    equipamento_id = table.Column<int>(type: "integer", nullable: false),
                    fornecedor_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedores_equipamentos", x => new { x.equipamento_id, x.fornecedor_id });
                    table.ForeignKey(
                        name: "FK_fornecedores_equipamentos_equipamento_equipamento_id",
                        column: x => x.equipamento_id,
                        principalTable: "equipamento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fornecedores_equipamentos_fornecedor_fornecedor_id",
                        column: x => x.fornecedor_id,
                        principalTable: "fornecedor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "colaboradores_telefones",
                columns: table => new
                {
                    colaborador_id = table.Column<int>(type: "integer", nullable: false),
                    telefone_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colaboradores_telefones", x => new { x.colaborador_id, x.telefone_id });
                    table.ForeignKey(
                        name: "FK_colaboradores_telefones_colaborador_colaborador_id",
                        column: x => x.colaborador_id,
                        principalTable: "colaborador",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_colaboradores_telefones_telefone_telefone_id",
                        column: x => x.telefone_id,
                        principalTable: "telefone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "companies_telefones",
                columns: table => new
                {
                    companie_id = table.Column<int>(type: "integer", nullable: false),
                    telefone_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies_telefones", x => new { x.companie_id, x.telefone_id });
                    table.ForeignKey(
                        name: "FK_companies_telefones_companie_companie_id",
                        column: x => x.companie_id,
                        principalTable: "companie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_companies_telefones_telefone_telefone_id",
                        column: x => x.telefone_id,
                        principalTable: "telefone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fornecedores_telefones",
                columns: table => new
                {
                    fornecedor_id = table.Column<int>(type: "integer", nullable: false),
                    telefone_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedores_telefones", x => new { x.fornecedor_id, x.telefone_id });
                    table.ForeignKey(
                        name: "FK_fornecedores_telefones_fornecedor_fornecedor_id",
                        column: x => x.fornecedor_id,
                        principalTable: "fornecedor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fornecedores_telefones_telefone_telefone_id",
                        column: x => x.telefone_id,
                        principalTable: "telefone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "insumo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    descricao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    unidade_id = table.Column<int>(type: "integer", nullable: false),
                    tipo = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("insumo_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_insumo_unidade_unidade_id",
                        column: x => x.unidade_id,
                        principalTable: "unidade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "colaboradores_enderecos",
                columns: table => new
                {
                    colaborador_id = table.Column<int>(type: "integer", nullable: false),
                    endereco_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colaboradores_enderecos", x => new { x.colaborador_id, x.endereco_id });
                    table.ForeignKey(
                        name: "FK_colaboradores_enderecos_colaborador_colaborador_id",
                        column: x => x.colaborador_id,
                        principalTable: "colaborador",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_colaboradores_enderecos_endereco_endereco_id",
                        column: x => x.endereco_id,
                        principalTable: "endereco",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "companies_enderecos",
                columns: table => new
                {
                    companie_id = table.Column<int>(type: "integer", nullable: false),
                    endereco_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies_enderecos", x => new { x.companie_id, x.endereco_id });
                    table.ForeignKey(
                        name: "FK_companies_enderecos_companie_companie_id",
                        column: x => x.companie_id,
                        principalTable: "companie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_companies_enderecos_endereco_endereco_id",
                        column: x => x.endereco_id,
                        principalTable: "endereco",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fornecedores_enderecos",
                columns: table => new
                {
                    endereco_id = table.Column<int>(type: "integer", nullable: false),
                    fornecedor_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedores_enderecos", x => new { x.endereco_id, x.fornecedor_id });
                    table.ForeignKey(
                        name: "FK_fornecedores_enderecos_endereco_endereco_id",
                        column: x => x.endereco_id,
                        principalTable: "endereco",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fornecedores_enderecos_fornecedor_fornecedor_id",
                        column: x => x.fornecedor_id,
                        principalTable: "fornecedor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "planejamento",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    obra_id = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("planejamento_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_planejamento_obra_obra_id",
                        column: x => x.obra_id,
                        principalTable: "obra",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedido",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    planejamento_id = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pedido_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_pedido_planejamento_planejamento_id",
                        column: x => x.planejamento_id,
                        principalTable: "planejamento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "planejamento_insumo",
                columns: table => new
                {
                    planejamento_id = table.Column<int>(type: "integer", nullable: false),
                    insumo_id = table.Column<int>(type: "integer", nullable: false),
                    valor_unitario = table.Column<decimal>(type: "numeric", nullable: false),
                    quantidade = table.Column<double>(type: "double precision", nullable: false),
                    data_inicio = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_fim = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planejamento_insumo", x => new { x.planejamento_id, x.insumo_id });
                    table.ForeignKey(
                        name: "FK_planejamento_insumo_insumo_insumo_id",
                        column: x => x.insumo_id,
                        principalTable: "insumo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_planejamento_insumo_planejamento_planejamento_id",
                        column: x => x.planejamento_id,
                        principalTable: "planejamento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "compra",
                columns: table => new
                {
                    pedido_id = table.Column<int>(type: "integer", nullable: false),
                    insumo_id = table.Column<int>(type: "integer", nullable: false),
                    fornecedor_id = table.Column<int>(type: "integer", nullable: false),
                    quantidade = table.Column<double>(type: "double precision", nullable: false),
                    valor_unitario = table.Column<decimal>(type: "numeric", nullable: false),
                    data_entrega = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    local_entrega_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compra", x => new { x.pedido_id, x.insumo_id });
                    table.ForeignKey(
                        name: "FK_compra_endereco_local_entrega_id",
                        column: x => x.local_entrega_id,
                        principalTable: "endereco",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_compra_fornecedor_fornecedor_id",
                        column: x => x.fornecedor_id,
                        principalTable: "fornecedor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_compra_insumo_insumo_id",
                        column: x => x.insumo_id,
                        principalTable: "insumo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_compra_pedido_pedido_id",
                        column: x => x.pedido_id,
                        principalTable: "pedido",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_colaboradores_emails_email_id",
                table: "colaboradores_emails",
                column: "email_id");

            migrationBuilder.CreateIndex(
                name: "IX_colaboradores_enderecos_endereco_id",
                table: "colaboradores_enderecos",
                column: "endereco_id");

            migrationBuilder.CreateIndex(
                name: "IX_colaboradores_telefones_telefone_id",
                table: "colaboradores_telefones",
                column: "telefone_id");

            migrationBuilder.CreateIndex(
                name: "IX_compra_fornecedor_id",
                table: "compra",
                column: "fornecedor_id");

            migrationBuilder.CreateIndex(
                name: "IX_compra_insumo_id",
                table: "compra",
                column: "insumo_id");

            migrationBuilder.CreateIndex(
                name: "IX_compra_local_entrega_id",
                table: "compra",
                column: "local_entrega_id");

            migrationBuilder.CreateIndex(
                name: "IX_companies_emails_companie_id",
                table: "companies_emails",
                column: "companie_id");

            migrationBuilder.CreateIndex(
                name: "IX_companies_enderecos_endereco_id",
                table: "companies_enderecos",
                column: "endereco_id");

            migrationBuilder.CreateIndex(
                name: "IX_companies_telefones_telefone_id",
                table: "companies_telefones",
                column: "telefone_id");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_cidade_id",
                table: "endereco",
                column: "cidade_id");

            migrationBuilder.CreateIndex(
                name: "IX_fornecedores_emails_fornecedor_id",
                table: "fornecedores_emails",
                column: "fornecedor_id");

            migrationBuilder.CreateIndex(
                name: "IX_fornecedores_enderecos_fornecedor_id",
                table: "fornecedores_enderecos",
                column: "fornecedor_id");

            migrationBuilder.CreateIndex(
                name: "IX_fornecedores_equipamentos_fornecedor_id",
                table: "fornecedores_equipamentos",
                column: "fornecedor_id");

            migrationBuilder.CreateIndex(
                name: "IX_fornecedores_telefones_telefone_id",
                table: "fornecedores_telefones",
                column: "telefone_id");

            migrationBuilder.CreateIndex(
                name: "IX_insumo_unidade_id",
                table: "insumo",
                column: "unidade_id");

            migrationBuilder.CreateIndex(
                name: "IX_obra_companie_id",
                table: "obra",
                column: "companie_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_planejamento_id",
                table: "pedido",
                column: "planejamento_id");

            migrationBuilder.CreateIndex(
                name: "IX_planejamento_obra_id",
                table: "planejamento",
                column: "obra_id");

            migrationBuilder.CreateIndex(
                name: "IX_planejamento_insumo_insumo_id",
                table: "planejamento_insumo",
                column: "insumo_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "colaboradores_emails");

            migrationBuilder.DropTable(
                name: "colaboradores_enderecos");

            migrationBuilder.DropTable(
                name: "colaboradores_telefones");

            migrationBuilder.DropTable(
                name: "compra");

            migrationBuilder.DropTable(
                name: "companies_emails");

            migrationBuilder.DropTable(
                name: "companies_enderecos");

            migrationBuilder.DropTable(
                name: "companies_telefones");

            migrationBuilder.DropTable(
                name: "fornecedores_emails");

            migrationBuilder.DropTable(
                name: "fornecedores_enderecos");

            migrationBuilder.DropTable(
                name: "fornecedores_equipamentos");

            migrationBuilder.DropTable(
                name: "fornecedores_telefones");

            migrationBuilder.DropTable(
                name: "planejamento_insumo");

            migrationBuilder.DropTable(
                name: "colaborador");

            migrationBuilder.DropTable(
                name: "pedido");

            migrationBuilder.DropTable(
                name: "email");

            migrationBuilder.DropTable(
                name: "endereco");

            migrationBuilder.DropTable(
                name: "equipamento");

            migrationBuilder.DropTable(
                name: "fornecedor");

            migrationBuilder.DropTable(
                name: "telefone");

            migrationBuilder.DropTable(
                name: "insumo");

            migrationBuilder.DropTable(
                name: "planejamento");

            migrationBuilder.DropTable(
                name: "cidade");

            migrationBuilder.DropTable(
                name: "unidade");

            migrationBuilder.DropTable(
                name: "obra");

            migrationBuilder.DropTable(
                name: "companie");
        }
    }
}
