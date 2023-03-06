using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommunityPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommunityId",
                table: "community_post",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_community_post_CommunityId",
                table: "community_post",
                column: "CommunityId");

            migrationBuilder.AddForeignKey(
                name: "FK_community_post_community_CommunityId",
                table: "community_post",
                column: "CommunityId",
                principalTable: "community",
                principalColumn: "community_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_community_post_community_CommunityId",
                table: "community_post");

            migrationBuilder.DropIndex(
                name: "IX_community_post_CommunityId",
                table: "community_post");

            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "community_post");
        }
    }
}
