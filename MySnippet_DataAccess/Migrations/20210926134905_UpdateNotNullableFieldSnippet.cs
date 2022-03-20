using Microsoft.EntityFrameworkCore.Migrations;

namespace MySnippet_DataAccess.Migrations
{
    public partial class UpdateNotNullableFieldSnippet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Snippets_AspNetUsers_UserCreateId",
                table: "Snippets");

            migrationBuilder.AlterColumn<string>(
                name: "UserCreateId",
                table: "Snippets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Snippets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Snippets_AspNetUsers_UserCreateId",
                table: "Snippets",
                column: "UserCreateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Snippets_AspNetUsers_UserCreateId",
                table: "Snippets");

            migrationBuilder.AlterColumn<string>(
                name: "UserCreateId",
                table: "Snippets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Snippets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Snippets_AspNetUsers_UserCreateId",
                table: "Snippets",
                column: "UserCreateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
