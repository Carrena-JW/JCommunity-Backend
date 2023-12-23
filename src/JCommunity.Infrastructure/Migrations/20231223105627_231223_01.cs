using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCommunity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _231223_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    NickName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(600)", maxLength: 600, nullable: false),
                    MemberStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedMemberId = table.Column<string>(type: "text", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedMemberId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "topics",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Sort = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<string>(type: "character varying(26)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedMemberId = table.Column<string>(type: "text", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedMemberId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_topics_members_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", nullable: false),
                    TopicId = table.Column<string>(type: "character varying(26)", nullable: false),
                    IsDraft = table.Column<bool>(type: "boolean", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    HasAttachments = table.Column<bool>(type: "boolean", nullable: false),
                    IsReported = table.Column<bool>(type: "boolean", nullable: false),
                    Sources = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<string>(type: "character varying(26)", nullable: false),
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
                        name: "FK_posts_topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "topic_tags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", nullable: false),
                    TopicId = table.Column<string>(type: "character varying(26)", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topic_tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_topic_tags_topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_comments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", nullable: false),
                    PostId = table.Column<string>(type: "character varying(26)", nullable: false),
                    ParentCommentId = table.Column<string>(type: "character varying(26)", nullable: true),
                    Contents = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AuthorId = table.Column<string>(type: "character varying(26)", nullable: false),
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
                name: "post_contents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", nullable: false),
                    PostId = table.Column<string>(type: "character varying(26)", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "text", nullable: false),
                    MainImageUrl = table.Column<string>(type: "text", nullable: false),
                    HtmlBody = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_contents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_contents_posts_PostId",
                        column: x => x.PostId,
                        principalTable: "posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_likes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", nullable: false),
                    PostId = table.Column<string>(type: "character varying(26)", nullable: false),
                    AuthorId = table.Column<string>(type: "character varying(26)", nullable: false),
                    IsLike = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOrUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    Id = table.Column<string>(type: "character varying(26)", nullable: false),
                    PostId = table.Column<string>(type: "character varying(26)", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Contents = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedOrUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorId = table.Column<string>(type: "character varying(26)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "post_comment_likes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", nullable: false),
                    CommentId = table.Column<string>(type: "character varying(26)", nullable: false),
                    AuthorId = table.Column<string>(type: "character varying(26)", nullable: false),
                    IsLike = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOrUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "post_content_attachments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(26)", nullable: false),
                    PostContentId = table.Column<string>(type: "character varying(26)", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    FileExtention = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_members_Email",
                table: "members",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_members_NickName",
                table: "members",
                column: "NickName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_post_comment_likes_AuthorId",
                table: "post_comment_likes",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_post_comment_likes_CommentId",
                table: "post_comment_likes",
                column: "CommentId");

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
                name: "IX_post_contents_PostId",
                table: "post_contents",
                column: "PostId",
                unique: true);

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
                name: "IX_posts_TopicId",
                table: "posts",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_topic_tags_TopicId",
                table: "topic_tags",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_topics_AuthorId",
                table: "topics",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_topics_Name",
                table: "topics",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post_comment_likes");

            migrationBuilder.DropTable(
                name: "post_content_attachments");

            migrationBuilder.DropTable(
                name: "post_likes");

            migrationBuilder.DropTable(
                name: "post_reports");

            migrationBuilder.DropTable(
                name: "topic_tags");

            migrationBuilder.DropTable(
                name: "post_comments");

            migrationBuilder.DropTable(
                name: "post_contents");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "topics");

            migrationBuilder.DropTable(
                name: "members");
        }
    }
}
