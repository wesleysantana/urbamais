using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Urbamais.Identity.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "login",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    usuario_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login", x => new { x.login_provider, x.provider_key });
                });

            migrationBuilder.CreateTable(
                name: "perfil",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: true),
                    nome_normalizado = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perfil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "perfis_permissoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    perfil_id = table.Column<string>(type: "text", nullable: true),
                    tipo_permissao = table.Column<string>(type: "text", nullable: true),
                    valor_permissao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perfis_permissoes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "token",
                columns: table => new
                {
                    usuario_id = table.Column<string>(type: "text", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    valor = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_token", x => new { x.usuario_id, x.login_provider, x.nome });
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    nome_normalizado = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false),
                    email_normalizado = table.Column<string>(type: "text", nullable: true),
                    email_confirmado = table.Column<bool>(type: "boolean", nullable: false),
                    senha = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    telefone_confirmado = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_habilitado = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enable = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios_perfis",
                columns: table => new
                {
                    usuario_id = table.Column<string>(type: "text", nullable: false),
                    perfil_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios_perfis", x => new { x.usuario_id, x.perfil_id });
                });

            migrationBuilder.CreateTable(
                name: "usuarios_permissoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    tipo_permissao = table.Column<string>(type: "text", nullable: true),
                    valor_permissao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios_permissoes", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "login");

            migrationBuilder.DropTable(
                name: "perfil");

            migrationBuilder.DropTable(
                name: "perfis_permissoes");

            migrationBuilder.DropTable(
                name: "token");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "usuarios_perfis");

            migrationBuilder.DropTable(
                name: "usuarios_permissoes");
        }
    }
}
