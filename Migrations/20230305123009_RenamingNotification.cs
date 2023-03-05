using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class RenamingNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_botification_product_product_id",
                table: "user_botification");

            migrationBuilder.DropForeignKey(
                name: "FK_user_botification_user_user_id",
                table: "user_botification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_botification",
                table: "user_botification");

            migrationBuilder.RenameTable(
                name: "user_botification",
                newName: "user_notification");

            migrationBuilder.RenameIndex(
                name: "IX_user_botification_user_id",
                table: "user_notification",
                newName: "IX_user_notification_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_botification_product_id",
                table: "user_notification",
                newName: "IX_user_notification_product_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_notification",
                table: "user_notification",
                column: "user_notification_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_notification_product_product_id",
                table: "user_notification",
                column: "product_id",
                principalTable: "product",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_notification_user_user_id",
                table: "user_notification",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_notification_product_product_id",
                table: "user_notification");

            migrationBuilder.DropForeignKey(
                name: "FK_user_notification_user_user_id",
                table: "user_notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_notification",
                table: "user_notification");

            migrationBuilder.RenameTable(
                name: "user_notification",
                newName: "user_botification");

            migrationBuilder.RenameIndex(
                name: "IX_user_notification_user_id",
                table: "user_botification",
                newName: "IX_user_botification_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_notification_product_id",
                table: "user_botification",
                newName: "IX_user_botification_product_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_botification",
                table: "user_botification",
                column: "user_notification_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_botification_product_product_id",
                table: "user_botification",
                column: "product_id",
                principalTable: "product",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_botification_user_user_id",
                table: "user_botification",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
