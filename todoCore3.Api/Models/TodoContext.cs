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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Flow>().Property(p => p.Pos).HasDefaultValue(65536);
      modelBuilder.Entity<TodoItem>().Property(p => p.Pos).HasDefaultValue(65536);
    }

    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Flow> Flows { get; set; }
    public DbSet<User> Users { get; set; }
  }
}
