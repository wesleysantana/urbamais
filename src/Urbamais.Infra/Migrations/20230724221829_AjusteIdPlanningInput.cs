using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urbamais.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AjusteIdPlanningInput : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_planning_inputs_unit_UnitId",
                table: "planning_inputs");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "planning_inputs",
                newName: "unit_id");

            migrationBuilder.RenameColumn(
                name: "numerical_order",
                table: "planning_inputs",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_planning_inputs_UnitId",
                table: "planning_inputs",
                newName: "IX_planning_inputs_unit_id");

            migrationBuilder.AddForeignKey(
                name: "FK_planning_inputs_unit_unit_id",
                table: "planning_inputs",
                column: "unit_id",
                principalTable: "unit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_planning_inputs_unit_unit_id",
                table: "planning_inputs");

            migrationBuilder.RenameColumn(
                name: "unit_id",
                table: "planning_inputs",
                newName: "UnitId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "planning_inputs",
                newName: "numerical_order");

            migrationBuilder.RenameIndex(
                name: "IX_planning_inputs_unit_id",
                table: "planning_inputs",
                newName: "IX_planning_inputs_UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_planning_inputs_unit_UnitId",
                table: "planning_inputs",
                column: "UnitId",
                principalTable: "unit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
