using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTopupToOtherTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "top_up_transaction");

            migrationBuilder.CreateTable(
                name: "other_transaction",
                columns: table => new
                {
                    other_transaction_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    wallet_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    transction_type = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_other_transaction", x => x.other_transaction_id);
                    table.ForeignKey(
                        name: "FK_other_transaction_user_wallet_wallet_id",
                        column: x => x.wallet_id,
                        principalTable: "user_wallet",
                        principalColumn: "wallet_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_other_transaction_wallet_id",
                table: "other_transaction",
                column: "wallet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "other_transaction");

            migrationBuilder.CreateTable(
                name: "top_up_transaction",
                columns: table => new
                {
                    top_up_transaction = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    wallet_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    date_topup = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_top_up_transaction", x => x.top_up_transaction);
                    table.ForeignKey(
                        name: "FK_top_up_transaction_user_wallet_wallet_id",
                        column: x => x.wallet_id,
                        principalTable: "user_wallet",
                        principalColumn: "wallet_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_top_up_transaction_wallet_id",
                table: "top_up_transaction",
                column: "wallet_id");
        }
    }
}
