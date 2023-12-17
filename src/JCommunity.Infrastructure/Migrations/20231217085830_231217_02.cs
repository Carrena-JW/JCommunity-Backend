using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCommunity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _231217_02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_post_contents_PostContentsId",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_PostContentsId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "PostContentsId",
                table: "posts");

            migrationBuilder.CreateIndex(
                name: "IX_post_contents_PostId",
                table: "post_contents",
                column: "PostId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_post_contents_posts_PostId",
                table: "post_contents",
                column: "PostId",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_contents_posts_PostId",
                table: "post_contents");

            migrationBuilder.DropIndex(
                name: "IX_post_contents_PostId",
                table: "post_contents");

            migrationBuilder.AddColumn<string>(
                name: "PostContentsId",
                table: "posts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_posts_PostContentsId",
                table: "posts",
                column: "PostContentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_post_contents_PostContentsId",
                table: "posts",
                column: "PostContentsId",
                principalTable: "post_contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
