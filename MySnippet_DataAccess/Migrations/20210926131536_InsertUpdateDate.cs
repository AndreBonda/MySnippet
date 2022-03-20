using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MySnippet_DataAccess.Migrations
{
    public partial class InsertUpdateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Update",
                table: "Snippets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Update",
                table: "Snippets");
        }
    }
}
