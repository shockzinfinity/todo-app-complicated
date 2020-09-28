using Microsoft.EntityFrameworkCore.Migrations;

namespace todoCore3.Api.Migrations
{
  public partial class CreateTodoItem : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: "TodoItems",
        columns: table => new
        {
          Id = table.Column<long>(nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
          Name = table.Column<string>(nullable: true),
          IsCompleted = table.Column<bool>(nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_TodoItems", x => x.Id);
        });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
        name: "TodoItems");
    }
  }
}
