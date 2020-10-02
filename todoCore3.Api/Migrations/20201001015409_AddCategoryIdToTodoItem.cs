using Microsoft.EntityFrameworkCore.Migrations;

namespace todoCore3.Api.Migrations
{
    public partial class AddCategoryIdToTodoItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "TodoItems",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "TodoItems");
        }
    }
}
