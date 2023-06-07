using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urbamais.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AjusteNomeFornecedoresTelefones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fornecedoers_telefones");

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

            migrationBuilder.CreateIndex(
                name: "IX_fornecedores_telefones_telefone_id",
                table: "fornecedores_telefones",
                column: "telefone_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fornecedores_telefones");

            migrationBuilder.CreateTable(
                name: "fornecedoers_telefones",
                columns: table => new
                {
                    fornecedor_id = table.Column<int>(type: "integer", nullable: false),
                    telefone_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedoers_telefones", x => new { x.fornecedor_id, x.telefone_id });
                    table.ForeignKey(
                        name: "FK_fornecedoers_telefones_fornecedor_fornecedor_id",
                        column: x => x.fornecedor_id,
                        principalTable: "fornecedor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fornecedoers_telefones_telefone_telefone_id",
                        column: x => x.telefone_id,
                        principalTable: "telefone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fornecedoers_telefones_telefone_id",
                table: "fornecedoers_telefones",
                column: "telefone_id");
        }
    }
}
