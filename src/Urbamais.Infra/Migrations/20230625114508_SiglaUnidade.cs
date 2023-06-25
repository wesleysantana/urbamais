using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urbamais.Infra.Migrations
{
    /// <inheritdoc />
    public partial class SiglaUnidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sigla",
                table: "unidade",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CNHId",
                table: "colaborador",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CarteiraTrabalhoId",
                table: "colaborador",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ExameAdmissionalId",
                table: "colaborador",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FichaEPIId",
                table: "colaborador",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FichaRegistroId",
                table: "colaborador",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrdemServicoId",
                table: "colaborador",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ArquivoVO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoArquivo = table.Column<int>(type: "integer", nullable: false),
                    Extensao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoVO", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_colaborador_CarteiraTrabalhoId",
                table: "colaborador",
                column: "CarteiraTrabalhoId");

            migrationBuilder.CreateIndex(
                name: "IX_colaborador_CNHId",
                table: "colaborador",
                column: "CNHId");

            migrationBuilder.CreateIndex(
                name: "IX_colaborador_ExameAdmissionalId",
                table: "colaborador",
                column: "ExameAdmissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_colaborador_FichaEPIId",
                table: "colaborador",
                column: "FichaEPIId");

            migrationBuilder.CreateIndex(
                name: "IX_colaborador_FichaRegistroId",
                table: "colaborador",
                column: "FichaRegistroId");

            migrationBuilder.CreateIndex(
                name: "IX_colaborador_OrdemServicoId",
                table: "colaborador",
                column: "OrdemServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_colaborador_ArquivoVO_CNHId",
                table: "colaborador",
                column: "CNHId",
                principalTable: "ArquivoVO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_colaborador_ArquivoVO_CarteiraTrabalhoId",
                table: "colaborador",
                column: "CarteiraTrabalhoId",
                principalTable: "ArquivoVO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_colaborador_ArquivoVO_ExameAdmissionalId",
                table: "colaborador",
                column: "ExameAdmissionalId",
                principalTable: "ArquivoVO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_colaborador_ArquivoVO_FichaEPIId",
                table: "colaborador",
                column: "FichaEPIId",
                principalTable: "ArquivoVO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_colaborador_ArquivoVO_FichaRegistroId",
                table: "colaborador",
                column: "FichaRegistroId",
                principalTable: "ArquivoVO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_colaborador_ArquivoVO_OrdemServicoId",
                table: "colaborador",
                column: "OrdemServicoId",
                principalTable: "ArquivoVO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_colaborador_ArquivoVO_CNHId",
                table: "colaborador");

            migrationBuilder.DropForeignKey(
                name: "FK_colaborador_ArquivoVO_CarteiraTrabalhoId",
                table: "colaborador");

            migrationBuilder.DropForeignKey(
                name: "FK_colaborador_ArquivoVO_ExameAdmissionalId",
                table: "colaborador");

            migrationBuilder.DropForeignKey(
                name: "FK_colaborador_ArquivoVO_FichaEPIId",
                table: "colaborador");

            migrationBuilder.DropForeignKey(
                name: "FK_colaborador_ArquivoVO_FichaRegistroId",
                table: "colaborador");

            migrationBuilder.DropForeignKey(
                name: "FK_colaborador_ArquivoVO_OrdemServicoId",
                table: "colaborador");

            migrationBuilder.DropTable(
                name: "ArquivoVO");

            migrationBuilder.DropIndex(
                name: "IX_colaborador_CarteiraTrabalhoId",
                table: "colaborador");

            migrationBuilder.DropIndex(
                name: "IX_colaborador_CNHId",
                table: "colaborador");

            migrationBuilder.DropIndex(
                name: "IX_colaborador_ExameAdmissionalId",
                table: "colaborador");

            migrationBuilder.DropIndex(
                name: "IX_colaborador_FichaEPIId",
                table: "colaborador");

            migrationBuilder.DropIndex(
                name: "IX_colaborador_FichaRegistroId",
                table: "colaborador");

            migrationBuilder.DropIndex(
                name: "IX_colaborador_OrdemServicoId",
                table: "colaborador");

            migrationBuilder.DropColumn(
                name: "sigla",
                table: "unidade");

            migrationBuilder.DropColumn(
                name: "CNHId",
                table: "colaborador");

            migrationBuilder.DropColumn(
                name: "CarteiraTrabalhoId",
                table: "colaborador");

            migrationBuilder.DropColumn(
                name: "ExameAdmissionalId",
                table: "colaborador");

            migrationBuilder.DropColumn(
                name: "FichaEPIId",
                table: "colaborador");

            migrationBuilder.DropColumn(
                name: "FichaRegistroId",
                table: "colaborador");

            migrationBuilder.DropColumn(
                name: "OrdemServicoId",
                table: "colaborador");
        }
    }
}
