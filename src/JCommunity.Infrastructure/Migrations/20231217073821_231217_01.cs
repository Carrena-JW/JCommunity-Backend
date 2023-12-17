using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCommunity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _231217_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "post_contents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<string>(type: "text", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "text", nullable: false),
                    MainImageUrl = table.Column<string>(type: "text", nullable: false),
                    HtmlBody = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_contents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "post_content_attachments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PostContentId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    FileExtention = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_content_attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_content_attachments_post_contents_PostContentId",
                        column: x => x.PostContentId,
                        principalTable: "post_contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TopicId = table.Column<string>(type: "text", nullable: false),
                    IsDraft = table.Column<bool>(type: "boolean", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    HasAttachments = table.Column<bool>(type: "boolean", nullable: false),
                    IsReported = table.Column<bool>(type: "boolean", nullable: false),
                    Sources = table.Column<string>(type: "text", nullable: false),
                    PostContentsId = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedMemberId = table.Column<string>(type: "text", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedMemberId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_posts_members_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_posts_post_contents_PostContentsId",
                        column: x => x.PostContentsId,
                        principalTable: "post_contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_posts_topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_comments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<string>(type: "text", nullable: false),
                    ParentCommentId = table.Column<string>(type: "text", nullable: true),
                    Contents = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: false),
                    CreatedOrUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_comments_members_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_comments_posts_PostId",
                        column: x => x.PostId,
                        principalTable: "posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_likes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: false),
                    IsLike = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_likes_members_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_likes_posts_PostId",
                        column: x => x.PostId,
                        principalTable: "posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_reports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Contents = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedOrUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_reports_members_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_reports_posts_PostId",
                        column: x => x.PostId,
                        principalTable: "posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_post_comments_AuthorId",
                table: "post_comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_post_comments_PostId",
                table: "post_comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_post_content_attachments_PostContentId",
                table: "post_content_attachments",
                column: "PostContentId");

            migrationBuilder.CreateIndex(
                name: "IX_post_likes_AuthorId",
                table: "post_likes",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_post_likes_PostId",
                table: "post_likes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_post_reports_AuthorId",
                table: "post_reports",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_post_reports_PostId",
                table: "post_reports",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_posts_AuthorId",
                table: "posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_posts_PostContentsId",
                table: "posts",
                column: "PostContentsId");

            migrationBuilder.CreateIndex(
                name: "IX_posts_TopicId",
                table: "posts",
                column: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post_comments");

            migrationBuilder.DropTable(
                name: "post_content_attachments");

            migrationBuilder.DropTable(
                name: "post_likes");

            migrationBuilder.DropTable(
                name: "post_reports");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "post_contents");
        }
    }
}
