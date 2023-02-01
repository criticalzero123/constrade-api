using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    productid = table.Column<int>(name: "product_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    posteruserid = table.Column<int>(name: "poster_user_id", type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    modelnumber = table.Column<string>(name: "model_number", type: "character varying(50)", maxLength: 50, nullable: true),
                    serialnumber = table.Column<string>(name: "serial_number", type: "character varying(50)", maxLength: 50, nullable: true),
                    gamegenre = table.Column<string>(name: "game_genre", type: "character varying(100)", maxLength: 100, nullable: false),
                    platform = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    thumbnailurl = table.Column<string>(name: "thumbnail_url", type: "text", nullable: false),
                    cash = table.Column<decimal>(type: "numeric", nullable: false),
                    item = table.Column<string>(type: "text", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp with time zone", nullable: false),
                    countfavorite = table.Column<int>(name: "count_favorite", type: "integer", nullable: false),
                    condition = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    preferetrade = table.Column<string>(name: "prefere_trade", type: "character varying(20)", maxLength: 20, nullable: false),
                    deliverymethod = table.Column<string>(name: "delivery_method", type: "character varying(20)", maxLength: 20, nullable: false),
                    location = table.Column<string>(type: "text", nullable: false),
                    productstatus = table.Column<string>(name: "product_status", type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.productid);
                    table.ForeignKey(
                        name: "FK_product_user_poster_user_id",
                        column: x => x.posteruserid,
                        principalTable: "user",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_poster_user_id",
                table: "product",
                column: "poster_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product");
        }
    }
}
