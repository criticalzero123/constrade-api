using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingFollowsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "follow",
                columns: table => new
                {
                    followid = table.Column<int>(name: "follow_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    followbyuserid = table.Column<int>(name: "follow_by_user_id", type: "integer", nullable: false),
                    followedbyuserid = table.Column<int>(name: "followed_by_user_id", type: "integer", nullable: false),
                    datefollowed = table.Column<DateTime>(name: "date_followed", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_follow", x => x.followid);
                    table.ForeignKey(
                        name: "FK_follow_user_follow_by_user_id",
                        column: x => x.followbyuserid,
                        principalTable: "user",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_follow_user_followed_by_user_id",
                        column: x => x.followedbyuserid,
                        principalTable: "user",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_follow_follow_by_user_id",
                table: "follow",
                column: "follow_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_follow_followed_by_user_id",
                table: "follow",
                column: "followed_by_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "follow");
        }
    }
}
