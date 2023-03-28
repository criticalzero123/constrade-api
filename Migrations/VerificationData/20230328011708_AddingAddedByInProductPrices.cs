using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstradeApi.Migrations.VerificationData
{
    /// <inheritdoc />
    public partial class AddingAddedByInProductPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddedBy",
                table: "product_prices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_product_prices_AddedBy",
                table: "product_prices",
                column: "AddedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_product_prices_admin_accounts_AddedBy",
                table: "product_prices",
                column: "AddedBy",
                principalTable: "admin_accounts",
                principalColumn: "AdminAccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_prices_admin_accounts_AddedBy",
                table: "product_prices");

            migrationBuilder.DropIndex(
                name: "IX_product_prices_AddedBy",
                table: "product_prices");

            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "product_prices");
        }
    }
}
