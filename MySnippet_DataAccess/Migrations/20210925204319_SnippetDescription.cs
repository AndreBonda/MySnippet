using Microsoft.EntityFrameworkCore.Migrations;

namespace MySnippet_DataAccess.Migrations
{
    public partial class SnippetDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Snippets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Snippets");
        }
    }
}
