using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace todoCore3.Api.Repository
{
  // use in event handlers
  public class UnitOfWork : IUnitOfWork
  {
    protected readonly DbContext _context;

    public UnitOfWork(DbContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task CompleteAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}
