using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using todoCore3.Api.Models.Auth.Entities;

namespace todoCore3.Api.Models
{
  public class TodoContext : DbContext
  {
    private readonly IConfiguration _configuration;

    public TodoContext(DbContextOptions<TodoContext> options, IConfiguration configuration) : base(options)
    {
      _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(_configuration.GetConnectionString("todoCore3Database"));
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
    public DbSet<Account> Accounts { get; set; }
  }
}
