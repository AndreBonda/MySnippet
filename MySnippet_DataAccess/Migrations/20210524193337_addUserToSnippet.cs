using Microsoft.EntityFrameworkCore.Migrations;

namespace MySnippet_DataAccess.Migrations
{
    public partial class addUserToSnippet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCreateId",
                table: "Snippets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Snippets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Snippets_UserCreateId",
                table: "Snippets",
                column: "UserCreateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Snippets_AspNetUsers_UserCreateId",
                table: "Snippets",
                column: "UserCreateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Snippets_AspNetUsers_UserCreateId",
                table: "Snippets");

            migrationBuilder.DropIndex(
                name: "IX_Snippets_UserCreateId",
                table: "Snippets");

            migrationBuilder.DropColumn(
                name: "UserCreateId",
                table: "Snippets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Snippets");
        }
    }
}
