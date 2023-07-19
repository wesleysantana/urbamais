using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urbamais.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UserModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "id_user_creation",
                table: "unit",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id_user_deletion",
                table: "unit",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_modification",
                table: "unit",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_creation",
                table: "supplier",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id_user_deletion",
                table: "supplier",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_modification",
                table: "supplier",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_creation",
                table: "planning",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id_user_deletion",
                table: "planning",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_modification",
                table: "planning",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_creation",
                table: "phone",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id_user_deletion",
                table: "phone",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_modification",
                table: "phone",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_creation",
                table: "order",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id_user_deletion",
                table: "order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_modification",
                table: "order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_creation",
                table: "input",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id_user_deletion",
                table: "input",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_modification",
                table: "input",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_creation",
                table: "equipment",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id_user_deletion",
                table: "equipment",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_modification",
                table: "equipment",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_creation",
                table: "email",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id_user_deletion",
                table: "email",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_modification",
                table: "email",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_creation",
                table: "construction",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id_user_deletion",
                table: "construction",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_modification",
                table: "construction",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_creation",
                table: "companie",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id_user_deletion",
                table: "companie",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_modification",
                table: "companie",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_creation",
                table: "collaborator",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id_user_deletion",
                table: "collaborator",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_modification",
                table: "collaborator",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_creation",
                table: "city",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id_user_deletion",
                table: "city",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_modification",
                table: "city",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_creation",
                table: "address",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id_user_deletion",
                table: "address",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_user_modification",
                table: "address",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id_user_creation",
                table: "unit");

            migrationBuilder.DropColumn(
                name: "id_user_deletion",
                table: "unit");

            migrationBuilder.DropColumn(
                name: "id_user_modification",
                table: "unit");

            migrationBuilder.DropColumn(
                name: "id_user_creation",
                table: "supplier");

            migrationBuilder.DropColumn(
                name: "id_user_deletion",
                table: "supplier");

            migrationBuilder.DropColumn(
                name: "id_user_modification",
                table: "supplier");

            migrationBuilder.DropColumn(
                name: "id_user_creation",
                table: "planning");

            migrationBuilder.DropColumn(
                name: "id_user_deletion",
                table: "planning");

            migrationBuilder.DropColumn(
                name: "id_user_modification",
                table: "planning");

            migrationBuilder.DropColumn(
                name: "id_user_creation",
                table: "phone");

            migrationBuilder.DropColumn(
                name: "id_user_deletion",
                table: "phone");

            migrationBuilder.DropColumn(
                name: "id_user_modification",
                table: "phone");

            migrationBuilder.DropColumn(
                name: "id_user_creation",
                table: "order");

            migrationBuilder.DropColumn(
                name: "id_user_deletion",
                table: "order");

            migrationBuilder.DropColumn(
                name: "id_user_modification",
                table: "order");

            migrationBuilder.DropColumn(
                name: "id_user_creation",
                table: "input");

            migrationBuilder.DropColumn(
                name: "id_user_deletion",
                table: "input");

            migrationBuilder.DropColumn(
                name: "id_user_modification",
                table: "input");

            migrationBuilder.DropColumn(
                name: "id_user_creation",
                table: "equipment");

            migrationBuilder.DropColumn(
                name: "id_user_deletion",
                table: "equipment");

            migrationBuilder.DropColumn(
                name: "id_user_modification",
                table: "equipment");

            migrationBuilder.DropColumn(
                name: "id_user_creation",
                table: "email");

            migrationBuilder.DropColumn(
                name: "id_user_deletion",
                table: "email");

            migrationBuilder.DropColumn(
                name: "id_user_modification",
                table: "email");

            migrationBuilder.DropColumn(
                name: "id_user_creation",
                table: "construction");

            migrationBuilder.DropColumn(
                name: "id_user_deletion",
                table: "construction");

            migrationBuilder.DropColumn(
                name: "id_user_modification",
                table: "construction");

            migrationBuilder.DropColumn(
                name: "id_user_creation",
                table: "companie");

            migrationBuilder.DropColumn(
                name: "id_user_deletion",
                table: "companie");

            migrationBuilder.DropColumn(
                name: "id_user_modification",
                table: "companie");

            migrationBuilder.DropColumn(
                name: "id_user_creation",
                table: "collaborator");

            migrationBuilder.DropColumn(
                name: "id_user_deletion",
                table: "collaborator");

            migrationBuilder.DropColumn(
                name: "id_user_modification",
                table: "collaborator");

            migrationBuilder.DropColumn(
                name: "id_user_creation",
                table: "city");

            migrationBuilder.DropColumn(
                name: "id_user_deletion",
                table: "city");

            migrationBuilder.DropColumn(
                name: "id_user_modification",
                table: "city");

            migrationBuilder.DropColumn(
                name: "id_user_creation",
                table: "address");

            migrationBuilder.DropColumn(
                name: "id_user_deletion",
                table: "address");

            migrationBuilder.DropColumn(
                name: "id_user_modification",
                table: "address");
        }
    }
}
