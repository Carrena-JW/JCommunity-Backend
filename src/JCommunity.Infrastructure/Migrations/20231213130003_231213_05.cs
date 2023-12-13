using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCommunity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _231213_05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicTagMap_topic_categories_TopicTagId",
                table: "TopicTagMap");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicTagMap_topics_TopicId",
                table: "TopicTagMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_topic_categories",
                table: "topic_categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TopicTagMap",
                table: "TopicTagMap");

            migrationBuilder.RenameTable(
                name: "topic_categories",
                newName: "topic_tags");

            migrationBuilder.RenameTable(
                name: "TopicTagMap",
                newName: "topic_tag_map");

            migrationBuilder.RenameIndex(
                name: "IX_topic_categories_Value",
                table: "topic_tags",
                newName: "IX_topic_tags_Value");

            migrationBuilder.RenameIndex(
                name: "IX_topic_categories_Name",
                table: "topic_tags",
                newName: "IX_topic_tags_Name");

            migrationBuilder.RenameIndex(
                name: "IX_TopicTagMap_TopicTagId",
                table: "topic_tag_map",
                newName: "IX_topic_tag_map_TopicTagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_topic_tags",
                table: "topic_tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_topic_tag_map",
                table: "topic_tag_map",
                columns: new[] { "TopicId", "TopicTagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_topic_tag_map_topic_tags_TopicTagId",
                table: "topic_tag_map",
                column: "TopicTagId",
                principalTable: "topic_tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_topic_tag_map_topics_TopicId",
                table: "topic_tag_map",
                column: "TopicId",
                principalTable: "topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_topic_tag_map_topic_tags_TopicTagId",
                table: "topic_tag_map");

            migrationBuilder.DropForeignKey(
                name: "FK_topic_tag_map_topics_TopicId",
                table: "topic_tag_map");

            migrationBuilder.DropPrimaryKey(
                name: "PK_topic_tags",
                table: "topic_tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_topic_tag_map",
                table: "topic_tag_map");

            migrationBuilder.RenameTable(
                name: "topic_tags",
                newName: "topic_categories");

            migrationBuilder.RenameTable(
                name: "topic_tag_map",
                newName: "TopicTagMap");

            migrationBuilder.RenameIndex(
                name: "IX_topic_tags_Value",
                table: "topic_categories",
                newName: "IX_topic_categories_Value");

            migrationBuilder.RenameIndex(
                name: "IX_topic_tags_Name",
                table: "topic_categories",
                newName: "IX_topic_categories_Name");

            migrationBuilder.RenameIndex(
                name: "IX_topic_tag_map_TopicTagId",
                table: "TopicTagMap",
                newName: "IX_TopicTagMap_TopicTagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_topic_categories",
                table: "topic_categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TopicTagMap",
                table: "TopicTagMap",
                columns: new[] { "TopicId", "TopicTagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TopicTagMap_topic_categories_TopicTagId",
                table: "TopicTagMap",
                column: "TopicTagId",
                principalTable: "topic_categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicTagMap_topics_TopicId",
                table: "TopicTagMap",
                column: "TopicId",
                principalTable: "topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
