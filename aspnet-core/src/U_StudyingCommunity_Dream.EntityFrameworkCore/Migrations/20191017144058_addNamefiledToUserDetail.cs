using Microsoft.EntityFrameworkCore.Migrations;

namespace U_StudyingCommunity_Dream.Migrations
{
    public partial class addNamefiledToUserDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "userDetails",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "userDetails");
        }
    }
}
