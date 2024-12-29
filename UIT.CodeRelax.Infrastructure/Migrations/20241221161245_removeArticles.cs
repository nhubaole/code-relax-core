using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UIT.CodeRelax.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeArticles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Xóa bảng articles
            migrationBuilder.DropTable(
                name: "articles");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

        }
    }
}
