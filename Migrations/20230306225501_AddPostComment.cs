using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPostComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "community_post_comment",
                columns: table => new
                {
                    community_post_comment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    community_post_id = table.Column<int>(type: "integer", nullable: false),
                    commented_by_user = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    date_commented = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_community_post_comment", x => x.community_post_comment_id);
                    table.ForeignKey(
                        name: "FK_community_post_comment_community_post_community_post_id",
                        column: x => x.community_post_id,
                        principalTable: "community_post",
                        principalColumn: "community_post_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_community_post_comment_user_commented_by_user",
                        column: x => x.commented_by_user,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_community_post_comment_commented_by_user",
                table: "community_post_comment",
                column: "commented_by_user");

            migrationBuilder.CreateIndex(
                name: "IX_community_post_comment_community_post_id",
                table: "community_post_comment",
                column: "community_post_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "community_post_comment");
        }
    }
}
