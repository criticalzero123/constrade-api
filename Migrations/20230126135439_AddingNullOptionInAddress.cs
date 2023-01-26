using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingNullOptionInAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_person_address_AddressRef_id",
                table: "person");

            migrationBuilder.AlterColumn<int>(
                name: "AddressRef_id",
                table: "person",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_person_address_AddressRef_id",
                table: "person",
                column: "AddressRef_id",
                principalTable: "address",
                principalColumn: "Address_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_person_address_AddressRef_id",
                table: "person");

            migrationBuilder.AlterColumn<int>(
                name: "AddressRef_id",
                table: "person",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_person_address_AddressRef_id",
                table: "person",
                column: "AddressRef_id",
                principalTable: "address",
                principalColumn: "Address_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
