using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace U_StudyingCommunity_Dream.Migrations
{
    public partial class FixInitDBCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "comments",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "comments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "comments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "comments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "comments",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "comments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "bookResource",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "bookResource",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "bookResource",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "bookResource",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "bookResource",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "bookResource",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "bookResource",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "bookResource",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "books");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "bookResource");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "bookResource");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "bookResource");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "bookResource");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "bookResource");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "bookResource");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "bookResource");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "bookResource");
        }
    }
}
