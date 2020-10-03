using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models
{
  public class TodoItemDTO
  {
    public long Id { get; set; }
    public long CategoryId { get; set; }
    [Required]
    public string Name { get; set; }
    [DefaultValue(false)]
    public bool IsComplete { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
