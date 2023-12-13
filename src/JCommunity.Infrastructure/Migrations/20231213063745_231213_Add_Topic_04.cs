using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCommunity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _231213_Add_Topic_04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_members_topics_AuthorId",
                table: "members");

            migrationBuilder.DropForeignKey(
                name: "FK_topic_categories_topics_TopicId",
                table: "topic_categories");

            migrationBuilder.DropIndex(
                name: "IX_topic_categories_TopicId",
                table: "topic_categories");

            migrationBuilder.DropIndex(
                name: "IX_members_AuthorId",
                table: "members");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "topic_categories");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "members");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "topics",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "topic_categories",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "TopicAuthorId",
                table: "members",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_topics_AuthorId",
                table: "topics",
                column: "AuthorId");

            migrationBuilder.CreateTable(
                name: "TopicTopicTag",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    TopicId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicTopicTag", x => new { x.CategoriesId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_TopicTopicTag_topic_categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "topic_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicTopicTag_topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_members_TopicAuthorId",
                table: "members",
                column: "TopicAuthorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopicTopicTag_TopicId",
                table: "TopicTopicTag",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_members_topics_TopicAuthorId",
                table: "members",
                column: "TopicAuthorId",
                principalTable: "topics",
                principalColumn: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_members_topics_TopicAuthorId",
                table: "members");

            migrationBuilder.DropTable(
                name: "TopicTopicTag");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_topics_AuthorId",
                table: "topics");

            migrationBuilder.DropIndex(
                name: "IX_members_TopicAuthorId",
                table: "members");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "topics");

            migrationBuilder.DropColumn(
                name: "TopicAuthorId",
                table: "members");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "topic_categories",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "TopicId",
                table: "topic_categories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "members",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_topic_categories_TopicId",
                table: "topic_categories",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_members_AuthorId",
                table: "members",
                column: "AuthorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_members_topics_AuthorId",
                table: "members",
                column: "AuthorId",
                principalTable: "topics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_topic_categories_topics_TopicId",
                table: "topic_categories",
                column: "TopicId",
                principalTable: "topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
