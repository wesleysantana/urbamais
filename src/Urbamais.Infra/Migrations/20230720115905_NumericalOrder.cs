using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urbamais.Infra.Migrations
{
    /// <inheritdoc />
    public partial class NumericalOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_planning_inputs",
                table: "planning_inputs");

            migrationBuilder.AddColumn<int>(
                name: "numerical_order",
                table: "planning_inputs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "planning_inputs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_planning_inputs",
                table: "planning_inputs",
                columns: new[] { "numerical_order", "planning_id", "input_id" });

            migrationBuilder.CreateIndex(
                name: "IX_planning_inputs_planning_id",
                table: "planning_inputs",
                column: "planning_id");

            migrationBuilder.CreateIndex(
                name: "IX_planning_inputs_UnitId",
                table: "planning_inputs",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_planning_inputs_unit_UnitId",
                table: "planning_inputs",
                column: "UnitId",
                principalTable: "unit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_planning_inputs_unit_UnitId",
                table: "planning_inputs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_planning_inputs",
                table: "planning_inputs");

            migrationBuilder.DropIndex(
                name: "IX_planning_inputs_planning_id",
                table: "planning_inputs");

            migrationBuilder.DropIndex(
                name: "IX_planning_inputs_UnitId",
                table: "planning_inputs");

            migrationBuilder.DropColumn(
                name: "numerical_order",
                table: "planning_inputs");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "planning_inputs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_planning_inputs",
                table: "planning_inputs",
                columns: new[] { "planning_id", "input_id" });
        }
    }
}
