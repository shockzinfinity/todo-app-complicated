using Microsoft.EntityFrameworkCore;

namespace todoCore3.Api.Models
{
  public class TodoContext : DbContext
  {
    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      //optionsBuilder.UseSqlServer("Data Source=sql;Database=todos;Integrated Security=false;User ID=sa;Password=p@ssw0rd;");
    }

    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<Category> Categories { get; set; }
  }
}
