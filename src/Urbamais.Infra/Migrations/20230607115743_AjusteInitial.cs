using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urbamais.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AjusteInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanejamentosInsumos_insumo_insumo_id",
                table: "PlanejamentosInsumos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanejamentosInsumos_planejamento_planejamneto_id",
                table: "PlanejamentosInsumos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanejamentosInsumos",
                table: "PlanejamentosInsumos");

            migrationBuilder.RenameTable(
                name: "PlanejamentosInsumos",
                newName: "planejamento_insumo");

            migrationBuilder.RenameColumn(
                name: "numero_exame_adminissional",
                table: "colaborador",
                newName: "numero_exame_admissional");

            migrationBuilder.RenameColumn(
                name: "planejamneto_id",
                table: "planejamento_insumo",
                newName: "planejamento_id");

            migrationBuilder.RenameIndex(
                name: "IX_PlanejamentosInsumos_insumo_id",
                table: "planejamento_insumo",
                newName: "IX_planejamento_insumo_insumo_id");

            migrationBuilder.AlterColumn<string>(
                name: "complemento",
                table: "endereco",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "tipo_cnh",
                table: "colaborador",
                type: "character varying(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "numero_cnh",
                table: "colaborador",
                type: "character varying(9)",
                maxLength: 9,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(9)",
                oldMaxLength: 9);

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_validade_exame_admissional",
                table: "colaborador",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_validade_cnh",
                table: "colaborador",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "numero_exame_admissional",
                table: "colaborador",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_planejamento_insumo",
                table: "planejamento_insumo",
                columns: new[] { "planejamento_id", "insumo_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_planejamento_insumo_insumo_insumo_id",
                table: "planejamento_insumo",
                column: "insumo_id",
                principalTable: "insumo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_planejamento_insumo_planejamento_planejamento_id",
                table: "planejamento_insumo",
                column: "planejamento_id",
                principalTable: "planejamento",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_planejamento_insumo_insumo_insumo_id",
                table: "planejamento_insumo");

            migrationBuilder.DropForeignKey(
                name: "FK_planejamento_insumo_planejamento_planejamento_id",
                table: "planejamento_insumo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_planejamento_insumo",
                table: "planejamento_insumo");

            migrationBuilder.RenameTable(
                name: "planejamento_insumo",
                newName: "PlanejamentosInsumos");

            migrationBuilder.RenameColumn(
                name: "numero_exame_admissional",
                table: "colaborador",
                newName: "numero_exame_adminissional");

            migrationBuilder.RenameColumn(
                name: "planejamento_id",
                table: "PlanejamentosInsumos",
                newName: "planejamneto_id");

            migrationBuilder.RenameIndex(
                name: "IX_planejamento_insumo_insumo_id",
                table: "PlanejamentosInsumos",
                newName: "IX_PlanejamentosInsumos_insumo_id");

            migrationBuilder.AlterColumn<string>(
                name: "complemento",
                table: "endereco",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tipo_cnh",
                table: "colaborador",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(2)",
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "numero_cnh",
                table: "colaborador",
                type: "character varying(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(9)",
                oldMaxLength: 9,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_validade_exame_admissional",
                table: "colaborador",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_validade_cnh",
                table: "colaborador",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "numero_exame_adminissional",
                table: "colaborador",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanejamentosInsumos",
                table: "PlanejamentosInsumos",
                columns: new[] { "planejamneto_id", "insumo_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlanejamentosInsumos_insumo_insumo_id",
                table: "PlanejamentosInsumos",
                column: "insumo_id",
                principalTable: "insumo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanejamentosInsumos_planejamento_planejamneto_id",
                table: "PlanejamentosInsumos",
                column: "planejamneto_id",
                principalTable: "planejamento",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
