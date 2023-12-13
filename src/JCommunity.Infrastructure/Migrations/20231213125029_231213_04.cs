using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCommunity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _231213_04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopicTagMap",
                columns: table => new
                {
                    TopicId = table.Column<string>(type: "text", nullable: false),
                    TopicTagId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicTagMap", x => new { x.TopicId, x.TopicTagId });
                    table.ForeignKey(
                        name: "FK_TopicTagMap_topic_categories_TopicTagId",
                        column: x => x.TopicTagId,
                        principalTable: "topic_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicTagMap_topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicTagMap_TopicTagId",
                table: "TopicTagMap",
                column: "TopicTagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopicTagMap");
        }
    }
}
