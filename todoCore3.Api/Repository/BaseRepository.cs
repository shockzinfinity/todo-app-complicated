using System;
using Microsoft.EntityFrameworkCore;

namespace todoCore3.Api.Repository
{
  public abstract class BaseRepository<TEntity> where TEntity : class
  {
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(DbContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _dbSet = _context.Set<TEntity>();
    }
  }
}
