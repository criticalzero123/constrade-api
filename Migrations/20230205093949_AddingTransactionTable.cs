using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingTransactionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    transactionid = table.Column<int>(name: "transaction_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    productid = table.Column<int>(name: "product_id", type: "integer", nullable: false),
                    buyeruserid = table.Column<int>(name: "buyer_user_id", type: "integer", nullable: false),
                    selleruserid = table.Column<int>(name: "seller_user_id", type: "integer", nullable: false),
                    inapptransaction = table.Column<bool>(name: "in_app_transaction", type: "boolean", nullable: false),
                    datetransaction = table.Column<DateTime>(name: "date_transaction", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.transactionid);
                    table.ForeignKey(
                        name: "FK_transactions_product_product_id",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transactions_user_buyer_user_id",
                        column: x => x.buyeruserid,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transactions_user_seller_user_id",
                        column: x => x.selleruserid,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_transactions_buyer_user_id",
                table: "transactions",
                column: "buyer_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_product_id",
                table: "transactions",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_seller_user_id",
                table: "transactions",
                column: "seller_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactions");
        }
    }
}
