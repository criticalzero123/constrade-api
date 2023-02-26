using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class FixingTypoInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "prefere_trade",
                table: "product",
                newName: "prefer_trade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "prefer_trade",
                table: "product",
                newName: "prefere_trade");
        }
    }
}
