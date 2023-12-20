using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCommunity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _231220_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "post_comment_likes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CommentId = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: false),
                    IsLike = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_comment_likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_comment_likes_members_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_comment_likes_post_comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "post_comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_post_comment_likes_AuthorId",
                table: "post_comment_likes",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_post_comment_likes_CommentId",
                table: "post_comment_likes",
                column: "CommentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post_comment_likes");
        }
    }
}
