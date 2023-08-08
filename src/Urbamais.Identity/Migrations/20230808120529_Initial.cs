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
                name: "identity_role",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "identity_role_claim",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", maxLength: 100, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    role_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    claim_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    claim_value = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_role_claim", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "identity_user",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    id_user_creation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    user_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    normalized_user_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    normalized_email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", maxLength: 100, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    security_stamp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enable = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "identity_user_claim",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", maxLength: 100, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    user_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    claim_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    claim_value = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_user_claim", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "identity_user_login",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    provider_key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    provider_display_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    user_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_user_login", x => new { x.login_provider, x.provider_key });
                });

            migrationBuilder.CreateTable(
                name: "identity_user_role",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    role_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_user_role", x => new { x.user_id, x.role_id });
                });

            migrationBuilder.CreateTable(
                name: "identity_user_token",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    login_provider = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    value = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_user_token", x => new { x.user_id, x.login_provider, x.name });
                });

            migrationBuilder.InsertData(
                table: "identity_role",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[] { "af92719d-1d7f-4c80-aadc-ead1e2ab3a9d", null, "developer", "DEVELOPER" });

            migrationBuilder.InsertData(
                table: "identity_user",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "creation_date", "deletion_date", "email", "email_confirmed", "id_user_creation", "id_user_deletion", "id_user_modification", "lockout_enable", "lockout_end", "modification_date", "name", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "f287461c-b983-418c-9b88-08b101e7201d", 0, "907f6382-3be2-4f18-9457-e82274ff3dbd", new DateTime(2023, 8, 8, 9, 5, 29, 421, DateTimeKind.Local).AddTicks(3949), null, "dev@metamais.com", true, "be5391f6-c590-436c-b588-4f9c1b767ebf", null, null, false, null, null, "", "DEV@METAMAIS.COM", "DEV@METAMAIS.COM", "AQAAAAIAAYagAAAAEDbilRCukhZzm0Sis/WbivRLzfBv50EmvJdKDApwEDVZNu2A0t4V9f3x4678W8KwtA==", null, false, "a805f93d-bd6b-4ae9-b98f-50092a147e70", false, "dev@metamais.com" });

            migrationBuilder.InsertData(
                table: "identity_user_role",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { "af92719d-1d7f-4c80-aadc-ead1e2ab3a9d", "f287461c-b983-418c-9b88-08b101e7201d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "identity_role");

            migrationBuilder.DropTable(
                name: "identity_role_claim");

            migrationBuilder.DropTable(
                name: "identity_user");

            migrationBuilder.DropTable(
                name: "identity_user_claim");

            migrationBuilder.DropTable(
                name: "identity_user_login");

            migrationBuilder.DropTable(
                name: "identity_user_role");

            migrationBuilder.DropTable(
                name: "identity_user_token");
        }
    }
}
