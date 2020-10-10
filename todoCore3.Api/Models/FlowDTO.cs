using System;

namespace todoCore3.Api.Models
{
  public class FlowDto
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public long Pos { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long CategoryId { get; set; }
  }
}
