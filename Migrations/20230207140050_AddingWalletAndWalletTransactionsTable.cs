using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingWalletAndWalletTransactionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_wallet",
                columns: table => new
                {
                    walletid = table.Column<int>(name: "wallet_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: false),
                    balance = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_wallet", x => x.walletid);
                    table.ForeignKey(
                        name: "FK_user_wallet_user_user_id",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "send_money_transaction",
                columns: table => new
                {
                    sendmoneytransaction = table.Column<int>(name: "send_money_transaction", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    senderwalletid = table.Column<int>(name: "sender_wallet_id", type: "integer", nullable: false),
                    receiverwalletid = table.Column<int>(name: "receiver_wallet_id", type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    datesend = table.Column<DateTime>(name: "date_send", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_send_money_transaction", x => x.sendmoneytransaction);
                    table.ForeignKey(
                        name: "FK_send_money_transaction_user_wallet_receiver_wallet_id",
                        column: x => x.receiverwalletid,
                        principalTable: "user_wallet",
                        principalColumn: "wallet_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_send_money_transaction_user_wallet_sender_wallet_id",
                        column: x => x.senderwalletid,
                        principalTable: "user_wallet",
                        principalColumn: "wallet_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "top_up_transaction",
                columns: table => new
                {
                    topuptransaction = table.Column<int>(name: "top_up_transaction", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    walletid = table.Column<int>(name: "wallet_id", type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    datetopup = table.Column<DateTime>(name: "date_topup", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_top_up_transaction", x => x.topuptransaction);
                    table.ForeignKey(
                        name: "FK_top_up_transaction_user_wallet_wallet_id",
                        column: x => x.walletid,
                        principalTable: "user_wallet",
                        principalColumn: "wallet_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_send_money_transaction_receiver_wallet_id",
                table: "send_money_transaction",
                column: "receiver_wallet_id");

            migrationBuilder.CreateIndex(
                name: "IX_send_money_transaction_sender_wallet_id",
                table: "send_money_transaction",
                column: "sender_wallet_id");

            migrationBuilder.CreateIndex(
                name: "IX_top_up_transaction_wallet_id",
                table: "top_up_transaction",
                column: "wallet_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_wallet_user_id",
                table: "user_wallet",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "send_money_transaction");

            migrationBuilder.DropTable(
                name: "top_up_transaction");

            migrationBuilder.DropTable(
                name: "user_wallet");
        }
    }
}
