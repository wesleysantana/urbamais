using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Urbamais.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    id_user_creation = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "text", nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "text", nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    uf = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("city_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "collaborator",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    number_ctps = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    number_cnh = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: true),
                    type_cnh = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    expiration_date_cnh = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    cnh = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    epi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ctps = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    number_admission_exam = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    expiration_date_admission_exam = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    admission_exam = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    registration_form = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    service_order = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    id_user_creation = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "text", nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "text", nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("collaborator_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "companie",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    id_user_creation = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "text", nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "text", nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    trade_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    corporate_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    state_registration = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    municipal_registration = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("companie_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "email",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    id_user_creation = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "text", nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "text", nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("email_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    id_user_creation = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "text", nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "text", nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("equipment_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "phone",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    id_user_creation = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "text", nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "text", nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("phone_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "supplier",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    id_user_creation = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "text", nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "text", nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    trade_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    corporate_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    state_registration = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    municipal_registration = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("supplier_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "unit",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    acronym = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    id_user_creation = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "text", nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "text", nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("unit_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    id_user_creation = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "text", nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "text", nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    thoroughfare = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    complement = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    neighborhood = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    zip_code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("address_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_address_city_city_id",
                        column: x => x.city_id,
                        principalTable: "city",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "construction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    companie_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    id_user_creation = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "text", nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "text", nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("construction_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_construction_companie_companie_id",
                        column: x => x.companie_id,
                        principalTable: "companie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "collaborators_emails",
                columns: table => new
                {
                    collaborator_id = table.Column<int>(type: "integer", nullable: false),
                    email_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collaborators_emails", x => new { x.collaborator_id, x.email_id });
                    table.ForeignKey(
                        name: "FK_collaborators_emails_collaborator_collaborator_id",
                        column: x => x.collaborator_id,
                        principalTable: "collaborator",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_collaborators_emails_email_email_id",
                        column: x => x.email_id,
                        principalTable: "email",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "companies_emails",
                columns: table => new
                {
                    companie_id = table.Column<int>(type: "integer", nullable: false),
                    email_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies_emails", x => new { x.companie_id, x.email_id });
                    table.ForeignKey(
                        name: "FK_companies_emails_companie_companie_id",
                        column: x => x.companie_id,
                        principalTable: "companie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_companies_emails_email_email_id",
                        column: x => x.email_id,
                        principalTable: "email",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "collaborators_phones",
                columns: table => new
                {
                    collaborator_id = table.Column<int>(type: "integer", nullable: false),
                    phone_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collaborators_phones", x => new { x.collaborator_id, x.phone_id });
                    table.ForeignKey(
                        name: "FK_collaborators_phones_collaborator_collaborator_id",
                        column: x => x.collaborator_id,
                        principalTable: "collaborator",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_collaborators_phones_phone_phone_id",
                        column: x => x.phone_id,
                        principalTable: "phone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "companies_phones",
                columns: table => new
                {
                    companie_id = table.Column<int>(type: "integer", nullable: false),
                    phone_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies_phones", x => new { x.companie_id, x.phone_id });
                    table.ForeignKey(
                        name: "FK_companies_phones_companie_companie_id",
                        column: x => x.companie_id,
                        principalTable: "companie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_companies_phones_phone_phone_id",
                        column: x => x.phone_id,
                        principalTable: "phone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "suppliers_emails",
                columns: table => new
                {
                    email_id = table.Column<int>(type: "integer", nullable: false),
                    supplier_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers_emails", x => new { x.email_id, x.supplier_id });
                    table.ForeignKey(
                        name: "FK_suppliers_emails_email_email_id",
                        column: x => x.email_id,
                        principalTable: "email",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_suppliers_emails_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "suppliers_equipaments",
                columns: table => new
                {
                    equipament_id = table.Column<int>(type: "integer", nullable: false),
                    supplier_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers_equipaments", x => new { x.equipament_id, x.supplier_id });
                    table.ForeignKey(
                        name: "FK_suppliers_equipaments_equipment_equipament_id",
                        column: x => x.equipament_id,
                        principalTable: "equipment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_suppliers_equipaments_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "suppliers_phones",
                columns: table => new
                {
                    phone_id = table.Column<int>(type: "integer", nullable: false),
                    supplier_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers_phones", x => new { x.phone_id, x.supplier_id });
                    table.ForeignKey(
                        name: "FK_suppliers_phones_phone_phone_id",
                        column: x => x.phone_id,
                        principalTable: "phone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_suppliers_phones_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "input",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    unit_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    id_user_creation = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "text", nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "text", nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("input_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_input_unit_unit_id",
                        column: x => x.unit_id,
                        principalTable: "unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "collaborators_addresses",
                columns: table => new
                {
                    address_id = table.Column<int>(type: "integer", nullable: false),
                    collaborator_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collaborators_addresses", x => new { x.address_id, x.collaborator_id });
                    table.ForeignKey(
                        name: "FK_collaborators_addresses_address_address_id",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_collaborators_addresses_collaborator_collaborator_id",
                        column: x => x.collaborator_id,
                        principalTable: "collaborator",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "companies_addresses",
                columns: table => new
                {
                    address_id = table.Column<int>(type: "integer", nullable: false),
                    companie_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies_addresses", x => new { x.address_id, x.companie_id });
                    table.ForeignKey(
                        name: "FK_companies_addresses_address_address_id",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_companies_addresses_companie_companie_id",
                        column: x => x.companie_id,
                        principalTable: "companie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "suppliers_addresses",
                columns: table => new
                {
                    address_id = table.Column<int>(type: "integer", nullable: false),
                    supplier_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers_addresses", x => new { x.address_id, x.supplier_id });
                    table.ForeignKey(
                        name: "FK_suppliers_addresses_address_address_id",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_suppliers_addresses_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "planning",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    construction_id = table.Column<int>(type: "integer", nullable: false),
                    id_user_creation = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "text", nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "text", nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("planning_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_planning_construction_construction_id",
                        column: x => x.construction_id,
                        principalTable: "construction",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    planning_id = table.Column<int>(type: "integer", nullable: false),
                    id_user_creation = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_user_modification = table.Column<string>(type: "text", nullable: true),
                    modification_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_user_deletion = table.Column<string>(type: "text", nullable: true),
                    deletion_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("order_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_planning_planning_id",
                        column: x => x.planning_id,
                        principalTable: "planning",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "planning_inputs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    planning_id = table.Column<int>(type: "integer", nullable: false),
                    input_id = table.Column<int>(type: "integer", nullable: false),
                    unitary_value = table.Column<decimal>(type: "numeric", nullable: false),
                    unit_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    final_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planning_inputs", x => new { x.id, x.planning_id, x.input_id });
                    table.ForeignKey(
                        name: "FK_planning_inputs_input_input_id",
                        column: x => x.input_id,
                        principalTable: "input",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_planning_inputs_planning_planning_id",
                        column: x => x.planning_id,
                        principalTable: "planning",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_planning_inputs_unit_unit_id",
                        column: x => x.unit_id,
                        principalTable: "unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "integer", nullable: false),
                    input_id = table.Column<int>(type: "integer", nullable: false),
                    supplier_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    unitary_value = table.Column<decimal>(type: "numeric", nullable: false),
                    delivery_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    delivery_place_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase", x => new { x.order_id, x.input_id });
                    table.ForeignKey(
                        name: "FK_purchase_address_delivery_place_id",
                        column: x => x.delivery_place_id,
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_input_input_id",
                        column: x => x.input_id,
                        principalTable: "input",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_order_id",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_address_city_id",
                table: "address",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_collaborators_addresses_collaborator_id",
                table: "collaborators_addresses",
                column: "collaborator_id");

            migrationBuilder.CreateIndex(
                name: "IX_collaborators_emails_email_id",
                table: "collaborators_emails",
                column: "email_id");

            migrationBuilder.CreateIndex(
                name: "IX_collaborators_phones_phone_id",
                table: "collaborators_phones",
                column: "phone_id");

            migrationBuilder.CreateIndex(
                name: "IX_companies_addresses_companie_id",
                table: "companies_addresses",
                column: "companie_id");

            migrationBuilder.CreateIndex(
                name: "IX_companies_emails_email_id",
                table: "companies_emails",
                column: "email_id");

            migrationBuilder.CreateIndex(
                name: "IX_companies_phones_phone_id",
                table: "companies_phones",
                column: "phone_id");

            migrationBuilder.CreateIndex(
                name: "IX_construction_companie_id",
                table: "construction",
                column: "companie_id");

            migrationBuilder.CreateIndex(
                name: "IX_input_unit_id",
                table: "input",
                column: "unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_planning_id",
                table: "order",
                column: "planning_id");

            migrationBuilder.CreateIndex(
                name: "IX_planning_construction_id",
                table: "planning",
                column: "construction_id");

            migrationBuilder.CreateIndex(
                name: "IX_planning_inputs_input_id",
                table: "planning_inputs",
                column: "input_id");

            migrationBuilder.CreateIndex(
                name: "IX_planning_inputs_planning_id",
                table: "planning_inputs",
                column: "planning_id");

            migrationBuilder.CreateIndex(
                name: "IX_planning_inputs_unit_id",
                table: "planning_inputs",
                column: "unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_delivery_place_id",
                table: "purchase",
                column: "delivery_place_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_input_id",
                table: "purchase",
                column: "input_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_supplier_id",
                table: "purchase",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_addresses_supplier_id",
                table: "suppliers_addresses",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_emails_supplier_id",
                table: "suppliers_emails",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_equipaments_supplier_id",
                table: "suppliers_equipaments",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_phones_supplier_id",
                table: "suppliers_phones",
                column: "supplier_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "collaborators_addresses");

            migrationBuilder.DropTable(
                name: "collaborators_emails");

            migrationBuilder.DropTable(
                name: "collaborators_phones");

            migrationBuilder.DropTable(
                name: "companies_addresses");

            migrationBuilder.DropTable(
                name: "companies_emails");

            migrationBuilder.DropTable(
                name: "companies_phones");

            migrationBuilder.DropTable(
                name: "planning_inputs");

            migrationBuilder.DropTable(
                name: "purchase");

            migrationBuilder.DropTable(
                name: "suppliers_addresses");

            migrationBuilder.DropTable(
                name: "suppliers_emails");

            migrationBuilder.DropTable(
                name: "suppliers_equipaments");

            migrationBuilder.DropTable(
                name: "suppliers_phones");

            migrationBuilder.DropTable(
                name: "collaborator");

            migrationBuilder.DropTable(
                name: "input");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "email");

            migrationBuilder.DropTable(
                name: "equipment");

            migrationBuilder.DropTable(
                name: "phone");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "unit");

            migrationBuilder.DropTable(
                name: "planning");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "construction");

            migrationBuilder.DropTable(
                name: "companie");
        }
    }
}
