using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urbamais.Identity.Migrations
{
    /// <inheritdoc />
    public partial class TipoIDRoleClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "perfil",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "perfil",
                newName: "Id");
        }
    }
}
