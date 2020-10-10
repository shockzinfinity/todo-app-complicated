using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace todoCore3.Api.Migrations
{
  public partial class UpdateCategoryAndTodoItem : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<DateTime>(
          name: "CreatedAt",
          table: "TodoItems",
          nullable: false,
          defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

      migrationBuilder.AddColumn<DateTime>(
          name: "UpdatedAt",
          table: "TodoItems",
          nullable: false,
          defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

      migrationBuilder.AddColumn<string>(
          name: "BgColor",
          table: "Categories",
          nullable: true);

      migrationBuilder.AddColumn<DateTime>(
          name: "CreatedAt",
          table: "Categories",
          nullable: false,
          defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

      migrationBuilder.AddColumn<DateTime>(
          name: "UpdatedAt",
          table: "Categories",
          nullable: false,
          defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

      migrationBuilder.AddColumn<int>(
          name: "UserId",
          table: "Categories",
          nullable: false,
          defaultValue: 0);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "CreatedAt",
          table: "TodoItems");

      migrationBuilder.DropColumn(
          name: "UpdatedAt",
          table: "TodoItems");

      migrationBuilder.DropColumn(
          name: "BgColor",
          table: "Categories");

      migrationBuilder.DropColumn(
          name: "CreatedAt",
          table: "Categories");

      migrationBuilder.DropColumn(
          name: "UpdatedAt",
          table: "Categories");

      migrationBuilder.DropColumn(
          name: "UserId",
          table: "Categories");
    }
  }
}
