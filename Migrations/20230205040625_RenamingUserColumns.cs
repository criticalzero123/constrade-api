using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class RenamingUserColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_person_PersonRef_id",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "User_type",
                table: "user",
                newName: "user_type");

            migrationBuilder.RenameColumn(
                name: "User_status",
                table: "user",
                newName: "user_status");

            migrationBuilder.RenameColumn(
                name: "Subscription_type",
                table: "user",
                newName: "subscription_type");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "user",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "user",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "LastActiveAt",
                table: "user",
                newName: "last_active_at");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "user",
                newName: "image_url");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "user",
                newName: "date_created");

            migrationBuilder.RenameColumn(
                name: "CountPost",
                table: "user",
                newName: "count_post");

            migrationBuilder.RenameColumn(
                name: "PersonRef_id",
                table: "user",
                newName: "person_ref_id");

            migrationBuilder.RenameColumn(
                name: "Authprovider_type",
                table: "user",
                newName: "auth_provider_type");

            migrationBuilder.RenameIndex(
                name: "IX_user_PersonRef_id",
                table: "user",
                newName: "IX_user_person_ref_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_person_person_ref_id",
                table: "user",
                column: "person_ref_id",
                principalTable: "person",
                principalColumn: "Person_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_person_person_ref_id",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "user_type",
                table: "user",
                newName: "User_type");

            migrationBuilder.RenameColumn(
                name: "user_status",
                table: "user",
                newName: "User_status");

            migrationBuilder.RenameColumn(
                name: "subscription_type",
                table: "user",
                newName: "Subscription_type");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "user",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "user",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "user",
                newName: "User_id");

            migrationBuilder.RenameColumn(
                name: "last_active_at",
                table: "user",
                newName: "LastActiveAt");

            migrationBuilder.RenameColumn(
                name: "image_url",
                table: "user",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "date_created",
                table: "user",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "count_post",
                table: "user",
                newName: "CountPost");

            migrationBuilder.RenameColumn(
                name: "person_ref_id",
                table: "user",
                newName: "PersonRef_id");

            migrationBuilder.RenameColumn(
                name: "auth_provider_type",
                table: "user",
                newName: "Authprovider_type");

            migrationBuilder.RenameIndex(
                name: "IX_user_person_ref_id",
                table: "user",
                newName: "IX_user_PersonRef_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_person_PersonRef_id",
                table: "user",
                column: "PersonRef_id",
                principalTable: "person",
                principalColumn: "Person_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
