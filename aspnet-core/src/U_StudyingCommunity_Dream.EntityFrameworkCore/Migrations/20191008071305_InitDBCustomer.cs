using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace U_StudyingCommunity_Dream.Migrations
{
    public partial class InitDBCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fans",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "Idols",
                table: "userDetails");

            migrationBuilder.RenameColumn(
                name: "headPortraitUrl",
                table: "userDetails",
                newName: "HeadPortraitUrl");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "userDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "userDetails",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "userDetails",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "userDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "userDetails",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "userDetails",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Site",
                table: "userDetails",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "userDetails",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "UserDetailId",
                table: "AbpUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "article_ArticleCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ArticleId = table.Column<long>(nullable: false),
                    ArticleCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article_ArticleCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "articleCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articleCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Headline = table.Column<string>(maxLength: 100, nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Praise = table.Column<long>(nullable: false),
                    VisitVolume = table.Column<long>(nullable: false),
                    ReleaseStatus = table.Column<int>(nullable: false),
                    UserDetailId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bookCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Parent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bookResource",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(maxLength: 200, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Author = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    CoverUrl = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(maxLength: 200, nullable: false),
                    UserDetailId = table.Column<Guid>(nullable: false),
                    Parent = table.Column<long>(nullable: false),
                    ArticleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fans",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    FansId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Progress = table.Column<decimal>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    Parent = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userDetail_Books",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    BookId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userDetail_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userDetail_Projects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userDetail_Projects", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_ArticleCategories");

            migrationBuilder.DropTable(
                name: "articleCategory");

            migrationBuilder.DropTable(
                name: "articles");

            migrationBuilder.DropTable(
                name: "bookCategories");

            migrationBuilder.DropTable(
                name: "bookResource");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "fans");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "userDetail_Books");

            migrationBuilder.DropTable(
                name: "userDetail_Projects");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "Site",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "UserDetailId",
                table: "AbpUsers");

            migrationBuilder.RenameColumn(
                name: "HeadPortraitUrl",
                table: "userDetails",
                newName: "headPortraitUrl");

            migrationBuilder.AddColumn<string>(
                name: "Fans",
                table: "userDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Idols",
                table: "userDetails",
                nullable: true);
        }
    }
}
