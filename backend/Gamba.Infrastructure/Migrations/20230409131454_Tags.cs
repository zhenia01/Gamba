using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamba.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Tags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "_creatorTags",
                schema: "users",
                table: "Users",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "_favoriteTags",
                schema: "users",
                table: "Users",
                type: "text[]",
                nullable: false,
                defaultValue: new List<string>());
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_creatorTags",
                schema: "users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "_favoriteTags",
                schema: "users",
                table: "Users");
        }
    }
}
