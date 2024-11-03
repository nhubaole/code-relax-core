using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UIT.CodeRelax.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addProblemCols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "function_name",
                table: "problems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "return_type",
                table: "problems",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "function_name",
                table: "problems");

            migrationBuilder.DropColumn(
                name: "return_type",
                table: "problems");
        }
    }
}
