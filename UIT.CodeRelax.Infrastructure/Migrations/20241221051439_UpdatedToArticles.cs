using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UIT.CodeRelax.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedToArticles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "like_count",
                table: "articles");

            migrationBuilder.AddColumn<string>(
                name: "cover",
                table: "articles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "articles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "subtitle",
                table: "articles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "summary",
                table: "articles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cover",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "subtitle",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "summary",
                table: "articles");

            migrationBuilder.AddColumn<int>(
                name: "like_count",
                table: "articles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
