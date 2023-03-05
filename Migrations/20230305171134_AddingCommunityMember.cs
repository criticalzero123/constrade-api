using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingCommunityMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "community_member",
                columns: table => new
                {
                    community_member_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    community_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    member_since = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_community_member", x => x.community_member_id);
                    table.ForeignKey(
                        name: "FK_community_member_community_community_id",
                        column: x => x.community_id,
                        principalTable: "community",
                        principalColumn: "community_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_member_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_community_member_community_id",
                table: "community_member",
                column: "community_id");

            migrationBuilder.CreateIndex(
                name: "IX_community_member_user_id",
                table: "community_member",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "community_member");
        }
    }
}
