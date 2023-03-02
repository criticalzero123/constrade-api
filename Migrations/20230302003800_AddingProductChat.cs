using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingProductChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product_chat",
                columns: table => new
                {
                    product_chat_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id_1 = table.Column<int>(type: "integer", nullable: false),
                    user_id_2 = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    last_message = table.Column<string>(type: "text", nullable: false),
                    last_message_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    chat_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_chat", x => x.product_chat_id);
                    table.ForeignKey(
                        name: "FK_product_chat_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_chat_user_user_id_1",
                        column: x => x.user_id_1,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_chat_user_user_id_2",
                        column: x => x.user_id_2,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_message",
                columns: table => new
                {
                    product_message_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_chat_id = table.Column<int>(type: "integer", nullable: false),
                    sender_id = table.Column<int>(type: "integer", nullable: false),
                    message = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    date_sent = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_message", x => x.product_message_id);
                    table.ForeignKey(
                        name: "FK_product_message_product_chat_product_chat_id",
                        column: x => x.product_chat_id,
                        principalTable: "product_chat",
                        principalColumn: "product_chat_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_message_user_sender_id",
                        column: x => x.sender_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_chat_product_id",
                table: "product_chat",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_chat_user_id_1",
                table: "product_chat",
                column: "user_id_1");

            migrationBuilder.CreateIndex(
                name: "IX_product_chat_user_id_2",
                table: "product_chat",
                column: "user_id_2");

            migrationBuilder.CreateIndex(
                name: "IX_product_message_product_chat_id",
                table: "product_message",
                column: "product_chat_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_message_sender_id",
                table: "product_message",
                column: "sender_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_message");

            migrationBuilder.DropTable(
                name: "product_chat");
        }
    }
}
