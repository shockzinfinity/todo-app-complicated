using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using todoCore3.Api.Models;

namespace todoCore3.Api.Repository
{
  public interface ITodoItemRepository
  {
    Task<IEnumerable<TodoItem>> ListAsync();
    Task AddAsync(TodoItem todoItem);
    Task<TodoItem> FindBy(long id);
    void Update(TodoItem todoItem);
    void Remove(TodoItem todoItem);
  }
}
