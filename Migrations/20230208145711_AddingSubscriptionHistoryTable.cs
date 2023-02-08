using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingSubscriptionHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "subscription_history",
                columns: table => new
                {
                    subscriptionhistoryid = table.Column<int>(name: "subscription_history_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: false),
                    subscriptiontype = table.Column<string>(name: "subscription_type", type: "text", nullable: false),
                    datestarted = table.Column<DateTime>(name: "date_started", type: "timestamp with time zone", nullable: false),
                    dateend = table.Column<DateTime>(name: "date_end", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscription_history", x => x.subscriptionhistoryid);
                    table.ForeignKey(
                        name: "FK_subscription_history_user_user_id",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_subscription_history_user_id",
                table: "subscription_history",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subscription_history");
        }
    }
}
