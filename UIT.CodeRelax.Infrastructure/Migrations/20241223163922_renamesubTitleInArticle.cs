using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UIT.CodeRelax.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renamesubTitleInArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Đổi tên cột từ subTitle thành sub_title trong bảng articles
            migrationBuilder.RenameColumn(
                name: "subTitle",
                table: "articles",
                newName: "sub_title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
           name: "sub_title",
           table: "articles",
           newName: "subTitle");
        }
    }
}
