using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UIT.CodeRelax.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeFKUserOfArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articles_users_user_id",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "articles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "articles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_articles_users_user_id",
                table: "articles",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
