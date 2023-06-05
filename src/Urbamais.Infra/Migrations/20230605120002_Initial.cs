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
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    uf = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cidade_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "email",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    endereco = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("email_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "telefone",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    numero = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("telefone_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "endereco",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    logradouro = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    numero = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    complemento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    bairro = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cidade_id = table.Column<int>(type: "integer", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_endereco_cidade_id",
                table: "endereco",
                column: "cidade_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "email");

            migrationBuilder.DropTable(
                name: "endereco");

            migrationBuilder.DropTable(
                name: "telefone");

            migrationBuilder.DropTable(
                name: "cidade");
        }
    }
}
