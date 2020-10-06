using System;

namespace todoCore3.Api.Models
{
  public class TodoItemDto
  {
    public long Id { get; set; }
    public long FlowId { get; set; }
    public long Pos { get; set; }
    public string Name { get; set; }
    public bool IsComplete { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
