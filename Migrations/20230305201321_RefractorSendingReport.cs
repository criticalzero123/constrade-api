using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstradeApi.Migrations
{
    /// <inheritdoc />
    public partial class RefractorSendingReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_report");

            migrationBuilder.DropTable(
                name: "user_report");

            migrationBuilder.CreateTable(
                name: "report",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reported_by = table.Column<int>(type: "integer", nullable: false),
                    id_reported = table.Column<int>(type: "integer", nullable: false),
                    report_type = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    date_submitted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_report_user_reported_by",
                        column: x => x.reported_by,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_report_reported_by",
                table: "report",
                column: "reported_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "report");

            migrationBuilder.CreateTable(
                name: "product_report",
                columns: table => new
                {
                    product_report_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_reported = table.Column<int>(type: "integer", nullable: false),
                    reported_by = table.Column<int>(type: "integer", nullable: false),
                    date_submitted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: false),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_report", x => x.product_report_id);
                    table.ForeignKey(
                        name: "FK_product_report_product_product_reported",
                        column: x => x.product_reported,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_report_user_reported_by",
                        column: x => x.reported_by,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_report",
                columns: table => new
                {
                    user_report_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    report_by = table.Column<int>(type: "integer", nullable: false),
                    reported = table.Column<int>(type: "integer", nullable: false),
                    date_submitted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    report_status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_report", x => x.user_report_id);
                    table.ForeignKey(
                        name: "FK_user_report_user_report_by",
                        column: x => x.report_by,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_report_user_reported",
                        column: x => x.reported,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_report_product_reported",
                table: "product_report",
                column: "product_reported");

            migrationBuilder.CreateIndex(
                name: "IX_product_report_reported_by",
                table: "product_report",
                column: "reported_by");

            migrationBuilder.CreateIndex(
                name: "IX_user_report_report_by",
                table: "user_report",
                column: "report_by");

            migrationBuilder.CreateIndex(
                name: "IX_user_report_reported",
                table: "user_report",
                column: "reported");
        }
    }
}
