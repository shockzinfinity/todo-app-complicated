using System;
using System.Collections.Generic;

namespace todoCore3.Api.Models
{
  public class CategoryDto
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public string BgColor { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }

  public class CategoryWithItems
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public string BgColor { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public IEnumerable<FlowWithItems> Lists { get; set; }
  }

  public class FlowWithItems
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public long Pos { get; set; }
    public long CategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IEnumerable<TodoItemDto> Items { get; set; }
  }
}
