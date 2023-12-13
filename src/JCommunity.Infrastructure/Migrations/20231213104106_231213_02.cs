using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCommunity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _231213_02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "topics",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_topics_Title",
                table: "topics",
                newName: "IX_topics_Name");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "topic_categories",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "topic_categories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "topic_categories");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "topics",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_topics_Name",
                table: "topics",
                newName: "IX_topics_Title");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "topic_categories",
                newName: "Category");
        }
    }
}
