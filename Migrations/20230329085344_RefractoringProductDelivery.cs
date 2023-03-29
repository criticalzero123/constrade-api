using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class RefractoringProductDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "delivery_method",
                table: "product");

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "product",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "is_delivery",
                table: "product",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_meetup",
                table: "product",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "product");

            migrationBuilder.DropColumn(
                name: "is_delivery",
                table: "product");

            migrationBuilder.DropColumn(
                name: "is_meetup",
                table: "product");

            migrationBuilder.AddColumn<string>(
                name: "delivery_method",
                table: "product",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
