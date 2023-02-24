using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingUserChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_chat",
                columns: table => new
                {
                    user_chat_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id_1 = table.Column<int>(type: "integer", nullable: false),
                    user_id_2 = table.Column<int>(type: "integer", nullable: false),
                    last_message = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    last_message_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_chat", x => x.user_chat_id);
                    table.ForeignKey(
                        name: "FK_user_chat_user_user_id_1",
                        column: x => x.user_id_1,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_chat_user_user_id_2",
                        column: x => x.user_id_2,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_message",
                columns: table => new
                {
                    user_message_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_chat_id = table.Column<int>(type: "integer", nullable: false),
                    sender_id = table.Column<int>(type: "integer", nullable: false),
                    message = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    date_sent = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_message", x => x.user_message_id);
                    table.ForeignKey(
                        name: "FK_user_message_user_chat_user_chat_id",
                        column: x => x.user_chat_id,
                        principalTable: "user_chat",
                        principalColumn: "user_chat_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_message_user_sender_id",
                        column: x => x.sender_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_chat_user_id_1",
                table: "user_chat",
                column: "user_id_1");

            migrationBuilder.CreateIndex(
                name: "IX_user_chat_user_id_2",
                table: "user_chat",
                column: "user_id_2");

            migrationBuilder.CreateIndex(
                name: "IX_user_message_sender_id",
                table: "user_message",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_message_user_chat_id",
                table: "user_message",
                column: "user_chat_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_message");

            migrationBuilder.DropTable(
                name: "user_chat");
        }
    }
}
