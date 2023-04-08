using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingBackgroundImageByUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_community_post_community_CommunityId",
                table: "community_post");

            migrationBuilder.RenameColumn(
                name: "CommunityId",
                table: "community_post",
                newName: "community_id");

            migrationBuilder.RenameIndex(
                name: "IX_community_post_CommunityId",
                table: "community_post",
                newName: "IX_community_post_community_id");

            migrationBuilder.AddColumn<string>(
                name: "background_image_url",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_community_post_community_community_id",
                table: "community_post",
                column: "community_id",
                principalTable: "community",
                principalColumn: "community_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_community_post_community_community_id",
                table: "community_post");

            migrationBuilder.DropColumn(
                name: "background_image_url",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "community_id",
                table: "community_post",
                newName: "CommunityId");

            migrationBuilder.RenameIndex(
                name: "IX_community_post_community_id",
                table: "community_post",
                newName: "IX_community_post_CommunityId");

            migrationBuilder.AddForeignKey(
                name: "FK_community_post_community_CommunityId",
                table: "community_post",
                column: "CommunityId",
                principalTable: "community",
                principalColumn: "community_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
