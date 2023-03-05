using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_notification_product_product_id",
                table: "user_notification");

            migrationBuilder.DropIndex(
                name: "IX_user_notification_product_id",
                table: "user_notification");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "user_notification",
                newName: "to_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "to_id",
                table: "user_notification",
                newName: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_notification_product_id",
                table: "user_notification",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_notification_product_product_id",
                table: "user_notification",
                column: "product_id",
                principalTable: "product",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
