using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace U_StudyingCommunity_Dream.Migrations
{
    public partial class editBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OtherUrls",
                table: "books",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Auditor",
                table: "bookResource",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BookId",
                table: "bookResource",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "Uploader",
                table: "bookResource",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "bookCategories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "books");

            migrationBuilder.DropColumn(
                name: "OtherUrls",
                table: "books");

            migrationBuilder.DropColumn(
                name: "Auditor",
                table: "bookResource");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "bookResource");

            migrationBuilder.DropColumn(
                name: "Uploader",
                table: "bookResource");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "bookCategories");
        }
    }
}
