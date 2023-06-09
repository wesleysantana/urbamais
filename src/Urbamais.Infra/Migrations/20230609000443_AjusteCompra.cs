using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urbamais.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AjusteCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_compra_endereco_local_entrega_id",
                table: "compra");

            migrationBuilder.AlterColumn<int>(
                name: "local_entrega_id",
                table: "compra",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_entrega",
                table: "compra",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_compra_endereco_local_entrega_id",
                table: "compra",
                column: "local_entrega_id",
                principalTable: "endereco",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_compra_endereco_local_entrega_id",
                table: "compra");

            migrationBuilder.AlterColumn<int>(
                name: "local_entrega_id",
                table: "compra",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_entrega",
                table: "compra",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_compra_endereco_local_entrega_id",
                table: "compra",
                column: "local_entrega_id",
                principalTable: "endereco",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
