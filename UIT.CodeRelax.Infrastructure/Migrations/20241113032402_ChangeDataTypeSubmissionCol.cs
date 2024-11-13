using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UIT.CodeRelax.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDataTypeSubmissionCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "language",
                table: "submissions",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "language",
                table: "submissions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
