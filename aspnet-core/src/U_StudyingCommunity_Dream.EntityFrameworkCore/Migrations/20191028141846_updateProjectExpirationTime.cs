using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace U_StudyingCommunity_Dream.Migrations
{
    public partial class updateProjectExpirationTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "projects");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "userDetail_Projects",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationTime",
                table: "projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "userDetail_Projects");

            migrationBuilder.DropColumn(
                name: "ExpirationTime",
                table: "projects");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "projects",
                nullable: false,
                defaultValue: false);
        }
    }
}
