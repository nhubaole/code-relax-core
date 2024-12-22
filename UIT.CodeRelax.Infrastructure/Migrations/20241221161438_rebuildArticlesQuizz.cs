﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UIT.CodeRelax.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rebuildArticlesQuizz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
             name: "articles",
             columns: table => new
             {
                 id = table.Column<int>(type: "integer", nullable: false)
                     .Annotation("Npgsql:Serial", true),
                 title = table.Column<string>(type: "text", nullable: true),
                 summary = table.Column<string>(type: "text", nullable: true),
                 subTitle = table.Column<string>(type: "jsonb", nullable: true),
                 cover = table.Column<string>(type: "text", nullable: true),
                 content = table.Column<string>(type: "jsonb", nullable: true),
                 created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                 updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                 user_id = table.Column<int>(type: "integer", nullable: false)
             },
            constraints: table =>
            {
                table.PrimaryKey("PK_articles", x => x.id);
                table.ForeignKey(
                    name: "FK_articles_users_user_id",
                    column: x => x.user_id,
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

            migrationBuilder.CreateTable(
                name: "quizzes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    question_text = table.Column<string>(type: "text", nullable: false),
                    option_a = table.Column<string>(type: "text", nullable: false),
                    option_b = table.Column<string>(type: "text", nullable: false),
                    option_c = table.Column<string>(type: "text", nullable: false),
                    option_d = table.Column<string>(type: "text", nullable: false),
                    correct_option = table.Column<string>(type: "text", nullable: false),
                    explanation = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    article_id = table.Column<int>(type: "integer", nullable: false),
                    articleId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizzes", x => x.id);
                    table.ForeignKey(
                        name: "FK_quizzes_articles_articleId",
                        column: x => x.articleId,
                        principalTable: "articles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_quizzes_articleId",
                table: "quizzes",
                column: "articleId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Xóa bảng articles
            migrationBuilder.DropTable(
                name: "articles");

            // Xóa bảng quizzes
            migrationBuilder.DropTable(
                name: "quizzes");

        }
    }
}