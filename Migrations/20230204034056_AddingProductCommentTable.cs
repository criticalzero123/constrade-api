using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingProductCommentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product_comment",
                columns: table => new
                {
                    productcommentid = table.Column<int>(name: "product_comment_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    productid = table.Column<int>(name: "product_id", type: "integer", nullable: false),
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: false),
                    comment = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_comment", x => x.productcommentid);
                    table.ForeignKey(
                        name: "FK_product_comment_product_product_id",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_comment_user_user_id",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_comment_product_id",
                table: "product_comment",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_comment_user_id",
                table: "product_comment",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_comment");
        }
    }
}
