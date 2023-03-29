using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingIsGeneratedInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "product",
                newName: "value");

            migrationBuilder.AddColumn<bool>(
                name: "is_generated",
                table: "product",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_generated",
                table: "product");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "product",
                newName: "Value");
        }
    }
}
