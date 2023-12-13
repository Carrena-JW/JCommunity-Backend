using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCommunity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _231213_03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "topic_categories",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_topic_categories_Name",
                table: "topic_categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_topic_categories_Value",
                table: "topic_categories",
                column: "Value",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_topic_categories_Name",
                table: "topic_categories");

            migrationBuilder.DropIndex(
                name: "IX_topic_categories_Value",
                table: "topic_categories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "topic_categories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);
        }
    }
}
