using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingJoinCommunity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_community_join_request_community_community_id",
                table: "community_join_request");

            migrationBuilder.DropForeignKey(
                name: "FK_community_join_request_user_user_id",
                table: "community_join_request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_community_join_request",
                table: "community_join_request");

            migrationBuilder.RenameTable(
                name: "community_join_request",
                newName: "community_join");

            migrationBuilder.RenameColumn(
                name: "community_join_request_id",
                table: "community_join",
                newName: "community_join_id");

            migrationBuilder.RenameIndex(
                name: "IX_community_join_request_user_id",
                table: "community_join",
                newName: "IX_community_join_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_community_join_request_community_id",
                table: "community_join",
                newName: "IX_community_join_community_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_community_join",
                table: "community_join",
                column: "community_join_id");

            migrationBuilder.AddForeignKey(
                name: "FK_community_join_community_community_id",
                table: "community_join",
                column: "community_id",
                principalTable: "community",
                principalColumn: "community_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_community_join_user_user_id",
                table: "community_join",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_community_join_community_community_id",
                table: "community_join");

            migrationBuilder.DropForeignKey(
                name: "FK_community_join_user_user_id",
                table: "community_join");

            migrationBuilder.DropPrimaryKey(
                name: "PK_community_join",
                table: "community_join");

            migrationBuilder.RenameTable(
                name: "community_join",
                newName: "community_join_request");

            migrationBuilder.RenameColumn(
                name: "community_join_id",
                table: "community_join_request",
                newName: "community_join_request_id");

            migrationBuilder.RenameIndex(
                name: "IX_community_join_user_id",
                table: "community_join_request",
                newName: "IX_community_join_request_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_community_join_community_id",
                table: "community_join_request",
                newName: "IX_community_join_request_community_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_community_join_request",
                table: "community_join_request",
                column: "community_join_request_id");

            migrationBuilder.AddForeignKey(
                name: "FK_community_join_request_community_community_id",
                table: "community_join_request",
                column: "community_id",
                principalTable: "community",
                principalColumn: "community_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_community_join_request_user_user_id",
                table: "community_join_request",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
