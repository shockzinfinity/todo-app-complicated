using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todoCore3.Api.Models;

namespace todoCore3.Api.Repository
{
  public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
  {
    public TodoItemRepository(DbContext context) :base(context)
    {
    }

    public async Task AddAsync(TodoItem todoItem)
    {
      await _dbSet.AddAsync(todoItem);
    }

    public async Task<TodoItem> FindBy(long id)
    {
      return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TodoItem>> ListAsync()
    {
      return await _dbSet.ToListAsync();
    }

    public void Remove(TodoItem todoItem)
    {
      _dbSet.Remove(todoItem);
    }

    public void Update(TodoItem todoItem)
    {
      _dbSet.Update(todoItem);
    }
  }
}
