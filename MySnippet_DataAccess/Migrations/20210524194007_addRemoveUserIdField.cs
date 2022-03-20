using Microsoft.EntityFrameworkCore.Migrations;

namespace MySnippet_DataAccess.Migrations
{
    public partial class addRemoveUserIdField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Snippets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Snippets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
