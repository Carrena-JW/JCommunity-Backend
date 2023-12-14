using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCommunity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _231215_02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_topic_tags_Name",
                table: "topic_tags");

            migrationBuilder.DropIndex(
                name: "IX_topic_tags_Value",
                table: "topic_tags");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_topic_tags_Name",
                table: "topic_tags",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_topic_tags_Value",
                table: "topic_tags",
                column: "Value",
                unique: true);
        }
    }
}
