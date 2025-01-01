using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UIT.CodeRelax.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateArticleFieldsToTextArray : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<string>>(
                name: "subtitle",
                table: "articles",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(JsonDocument),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<List<string>>(
                name: "content",
                table: "articles",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(JsonDocument),
                oldType: "jsonb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<JsonDocument>(
                name: "subtitle",
                table: "articles",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]");

            migrationBuilder.AlterColumn<JsonDocument>(
                name: "content",
                table: "articles",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]");
        }
    }
}
