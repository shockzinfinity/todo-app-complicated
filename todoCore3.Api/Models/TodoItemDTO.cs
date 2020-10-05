using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models
{
  public class TodoItemDTO
  {
    public long Id { get; set; }
    //public long CategoryId { get; set; }
    public long FlowId { get; set; }
    public long Post { get; set; }
    public string Name { get; set; }
    public bool IsComplete { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
