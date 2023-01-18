using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    Addressid = table.Column<int>(name: "Address_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Street = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Barangay = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Province = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Postalcode = table.Column<string>(name: "Postal_code", type: "character varying(50)", maxLength: 50, nullable: false),
                    Housenumber = table.Column<string>(name: "House_number", type: "character varying(50)", maxLength: 50, nullable: false),
                    Updatedat = table.Column<string>(name: "Updated_at", type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.Addressid);
                });

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    Personid = table.Column<int>(name: "Person_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AddressRefid = table.Column<int>(name: "AddressRef_id", type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.Personid);
                    table.ForeignKey(
                        name: "FK_person_address_AddressRef_id",
                        column: x => x.AddressRefid,
                        principalTable: "address",
                        principalColumn: "Address_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Userid = table.Column<int>(name: "User_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonRefid = table.Column<int>(name: "PersonRef_id", type: "integer", nullable: false),
                    Usertype = table.Column<string>(name: "User_type", type: "character varying(20)", maxLength: 20, nullable: false),
                    Authprovidertype = table.Column<string>(name: "Authprovider_type", type: "character varying(20)", maxLength: 20, nullable: false),
                    Subscriptiontype = table.Column<string>(name: "Subscription_type", type: "character varying(20)", maxLength: 20, nullable: false),
                    Userstatus = table.Column<string>(name: "User_status", type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LastActiveAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CountPost = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Userid);
                    table.ForeignKey(
                        name: "FK_user_person_PersonRef_id",
                        column: x => x.PersonRefid,
                        principalTable: "person",
                        principalColumn: "Person_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_person_AddressRef_id",
                table: "person",
                column: "AddressRef_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_PersonRef_id",
                table: "user",
                column: "PersonRef_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "person");

            migrationBuilder.DropTable(
                name: "address");
        }
    }
}
