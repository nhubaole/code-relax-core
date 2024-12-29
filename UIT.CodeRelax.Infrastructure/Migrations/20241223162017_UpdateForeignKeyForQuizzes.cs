using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UIT.CodeRelax.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForeignKeyForQuizzes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_quizzes_articles_articleId",  // khóa ngoại trỏ đến articleId cũ
            table: "quizzes");

            // Xóa cột articleId
            migrationBuilder.DropColumn(
                name: "articleId",  // cột cũ cần xóa
                table: "quizzes");

            // Thêm lại khóa ngoại cho cột article_id
            migrationBuilder.AddForeignKey(
                name: "FK_quizzes_articles_article_id", // khóa ngoại mới trỏ đến article_id
                table: "quizzes",
                column: "article_id",
                principalTable: "articles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_quizzes_articles_article_id", // khóa ngoại mới
            table: "quizzes");

            // Thêm lại cột articleId
            migrationBuilder.AddColumn<int>(
                name: "articleId",
                table: "quizzes",
                type: "integer",
                nullable: true);

            // Thêm lại khóa ngoại trỏ đến articleId
            migrationBuilder.AddForeignKey(
                name: "FK_quizzes_articles_articleId", // khóa ngoại cũ
                table: "quizzes",
                column: "articleId",
                principalTable: "articles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict); // có thể thay thế Restrict bằng Cascade tùy yêu cầu
        }
    }
}
