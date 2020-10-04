using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todoCore3.Api.Models
{
  public class CategoryWithItems
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public string BgColor { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public IEnumerable<TodoItemDTO> TodoItems { get; set; }
  }
}
