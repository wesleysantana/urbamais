using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urbamais.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddNameUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "identity_user",
                keyColumn: "id",
                keyValue: "9de47cee-b5de-45e3-af71-630970b526d6");

            migrationBuilder.DeleteData(
                table: "identity_user_role",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { "af92719d-1d7f-4c80-aadc-ead1e2ab3a9d", "9de47cee-b5de-45e3-af71-630970b526d6" });

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "identity_user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "identity_user",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "creation_date", "deletion_date", "email", "email_confirmed", "id_user_creation", "id_user_deletion", "id_user_modification", "lockout_enable", "lockout_end", "modification_date", "Name", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "f116fd17-1671-4c4e-80ca-a754ecc0a67f", 0, "9be65df2-d359-4d5f-8042-5d8c030e5184", new DateTime(2023, 7, 17, 9, 44, 13, 405, DateTimeKind.Local).AddTicks(5275), null, "dev@metamais.com", true, "6581df02-66a0-468c-83b2-a939671a25c5", null, null, false, null, null, "", "DEV@METAMAIS.COM", "DEV@METAMAIS.COM", "AQAAAAIAAYagAAAAEAXqd1ejCgLfn/RmlNaBIdjKlh35cl8MulHapxF6YDs2FIZ6Gi6ZGF6xYXVq6+z1Bg==", null, false, "46444d1b-65d7-41d4-9dda-1bd10e132cf5", false, "dev@metamais.com" });

            migrationBuilder.InsertData(
                table: "identity_user_role",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { "af92719d-1d7f-4c80-aadc-ead1e2ab3a9d", "f116fd17-1671-4c4e-80ca-a754ecc0a67f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "identity_user",
                keyColumn: "id",
                keyValue: "f116fd17-1671-4c4e-80ca-a754ecc0a67f");

            migrationBuilder.DeleteData(
                table: "identity_user_role",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { "af92719d-1d7f-4c80-aadc-ead1e2ab3a9d", "f116fd17-1671-4c4e-80ca-a754ecc0a67f" });

            migrationBuilder.DropColumn(
                name: "Name",
                table: "identity_user");

            migrationBuilder.InsertData(
                table: "identity_user",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "creation_date", "deletion_date", "email", "email_confirmed", "id_user_creation", "id_user_deletion", "id_user_modification", "lockout_enable", "lockout_end", "modification_date", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "9de47cee-b5de-45e3-af71-630970b526d6", 0, "52bb7a9d-d612-47ed-8994-c2389682dfbe", new DateTime(2023, 7, 16, 20, 43, 9, 275, DateTimeKind.Local).AddTicks(5967), null, "dev@metamais.com", false, "30f1dd45-b5d7-44bf-90e2-ca68e66e076d", null, null, false, null, null, "DEV@METAMAIS.COM", "DEV@METAMAIS.COM", "AQAAAAIAAYagAAAAEFKYrwwrYlFfMd6uWseLo8nA41tIafHbKBza+hOc4JK+koAa64ZBiE+bwfOBxAue0Q==", null, false, "e46f17e8-9daf-48ca-b990-bb2a4586d38c", false, "dev@metamais.com" });

            migrationBuilder.InsertData(
                table: "identity_user_role",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { "af92719d-1d7f-4c80-aadc-ead1e2ab3a9d", "9de47cee-b5de-45e3-af71-630970b526d6" });
        }
    }
}
