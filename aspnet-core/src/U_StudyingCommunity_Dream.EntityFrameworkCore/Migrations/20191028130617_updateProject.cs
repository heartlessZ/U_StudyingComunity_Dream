using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace U_StudyingCommunity_Dream.Migrations
{
    public partial class updateProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Node",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "ParentNode",
                table: "projects");

            migrationBuilder.AddColumn<string>(
                name: "TagName",
                table: "userDetail_Projects",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Parent",
                table: "projects",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "projects",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagName",
                table: "userDetail_Projects");

            migrationBuilder.DropColumn(
                name: "Parent",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "projects");

            migrationBuilder.AddColumn<Guid>(
                name: "Node",
                table: "projects",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ParentNode",
                table: "projects",
                nullable: true);
        }
    }
}
