using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCommunity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _231215_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_members_topics_TopicAuthorId",
                table: "members");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_topics_AuthorId",
                table: "topics");

            migrationBuilder.DropIndex(
                name: "IX_members_TopicAuthorId",
                table: "members");

            migrationBuilder.DropColumn(
                name: "TopicAuthorId",
                table: "members");

            migrationBuilder.CreateIndex(
                name: "IX_topics_AuthorId",
                table: "topics",
                column: "AuthorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_topics_members_AuthorId",
                table: "topics",
                column: "AuthorId",
                principalTable: "members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_topics_members_AuthorId",
                table: "topics");

            migrationBuilder.DropIndex(
                name: "IX_topics_AuthorId",
                table: "topics");

            migrationBuilder.AddColumn<string>(
                name: "TopicAuthorId",
                table: "members",
                type: "text",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_topics_AuthorId",
                table: "topics",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_members_TopicAuthorId",
                table: "members",
                column: "TopicAuthorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_members_topics_TopicAuthorId",
                table: "members",
                column: "TopicAuthorId",
                principalTable: "topics",
                principalColumn: "AuthorId");
        }
    }
}
