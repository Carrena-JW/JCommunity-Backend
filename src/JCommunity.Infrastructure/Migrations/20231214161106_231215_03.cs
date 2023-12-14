using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCommunity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _231215_03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_topics_AuthorId",
                table: "topics");

            migrationBuilder.CreateIndex(
                name: "IX_topics_AuthorId",
                table: "topics",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_topics_AuthorId",
                table: "topics");

            migrationBuilder.CreateIndex(
                name: "IX_topics_AuthorId",
                table: "topics",
                column: "AuthorId",
                unique: true);
        }
    }
}
