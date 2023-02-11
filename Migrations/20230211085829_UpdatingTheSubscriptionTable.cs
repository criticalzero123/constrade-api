using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingTheSubscriptionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subscription_history_user_user_id",
                table: "subscription_history");

            migrationBuilder.DropColumn(
                name: "subscription_type",
                table: "user");

            migrationBuilder.DropColumn(
                name: "status",
                table: "subscription_history");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "subscription_history",
                newName: "subscription_id");

            migrationBuilder.RenameColumn(
                name: "subscription_type",
                table: "subscription_history",
                newName: "previous_subscription_type");

            migrationBuilder.RenameColumn(
                name: "date_started",
                table: "subscription_history",
                newName: "previous_date_start");

            migrationBuilder.RenameColumn(
                name: "date_end",
                table: "subscription_history",
                newName: "previous_date_end");

            migrationBuilder.RenameIndex(
                name: "IX_subscription_history_user_id",
                table: "subscription_history",
                newName: "IX_subscription_history_subscription_id");

            migrationBuilder.AddColumn<decimal>(
                name: "new_amount",
                table: "subscription_history",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "new_date_end",
                table: "subscription_history",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "new_date_start",
                table: "subscription_history",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "new_subscription_type",
                table: "subscription_history",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "previous_amount",
                table: "subscription_history",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "subscription",
                columns: table => new
                {
                    subscriptionid = table.Column<int>(name: "subscription_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: false),
                    subscriptiontype = table.Column<string>(name: "subscription_type", type: "text", nullable: false),
                    datestart = table.Column<DateTime>(name: "date_start", type: "timestamp with time zone", nullable: false),
                    dateend = table.Column<DateTime>(name: "date_end", type: "timestamp with time zone", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscription", x => x.subscriptionid);
                    table.ForeignKey(
                        name: "FK_subscription_user_user_id",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_subscription_user_id",
                table: "subscription",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_subscription_history_subscription_subscription_id",
                table: "subscription_history",
                column: "subscription_id",
                principalTable: "subscription",
                principalColumn: "subscription_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subscription_history_subscription_subscription_id",
                table: "subscription_history");

            migrationBuilder.DropTable(
                name: "subscription");

            migrationBuilder.DropColumn(
                name: "new_amount",
                table: "subscription_history");

            migrationBuilder.DropColumn(
                name: "new_date_end",
                table: "subscription_history");

            migrationBuilder.DropColumn(
                name: "new_date_start",
                table: "subscription_history");

            migrationBuilder.DropColumn(
                name: "new_subscription_type",
                table: "subscription_history");

            migrationBuilder.DropColumn(
                name: "previous_amount",
                table: "subscription_history");

            migrationBuilder.RenameColumn(
                name: "subscription_id",
                table: "subscription_history",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "previous_subscription_type",
                table: "subscription_history",
                newName: "subscription_type");

            migrationBuilder.RenameColumn(
                name: "previous_date_start",
                table: "subscription_history",
                newName: "date_started");

            migrationBuilder.RenameColumn(
                name: "previous_date_end",
                table: "subscription_history",
                newName: "date_end");

            migrationBuilder.RenameIndex(
                name: "IX_subscription_history_subscription_id",
                table: "subscription_history",
                newName: "IX_subscription_history_user_id");

            migrationBuilder.AddColumn<string>(
                name: "subscription_type",
                table: "user",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "subscription_history",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_subscription_history_user_user_id",
                table: "subscription_history",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
