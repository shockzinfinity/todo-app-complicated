using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace todoCore3.Api.Migrations
{
  public partial class AddFlowAndUpdate : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "CategoryId",
          table: "TodoItems");

      migrationBuilder.AddColumn<long>(
          name: "FlowId",
          table: "TodoItems",
          nullable: false,
          defaultValue: 0L);

      migrationBuilder.AddColumn<long>(
          name: "Pos",
          table: "TodoItems",
          nullable: false,
          defaultValue: 65536L);

      migrationBuilder.CreateTable(
          name: "Flows",
          columns: table => new
          {
            Id = table.Column<long>(nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(nullable: false),
            Pos = table.Column<long>(nullable: false, defaultValue: 65536L),
            CreatedAt = table.Column<DateTime>(nullable: false),
            UpdatedAt = table.Column<DateTime>(nullable: false),
            RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
            CategoryId = table.Column<long>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Flows", x => x.Id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Flows");

      migrationBuilder.DropColumn(
          name: "FlowId",
          table: "TodoItems");

      migrationBuilder.DropColumn(
          name: "Pos",
          table: "TodoItems");

      migrationBuilder.AddColumn<long>(
          name: "CategoryId",
          table: "TodoItems",
          type: "bigint",
          nullable: false,
          defaultValue: 0L);
    }
  }
}
