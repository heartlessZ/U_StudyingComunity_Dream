using Microsoft.EntityFrameworkCore.Migrations;

namespace U_StudyingCommunity_Dream.Migrations
{
    public partial class addUserStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                table: "userDetails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enable",
                table: "userDetails");
        }
    }
}
