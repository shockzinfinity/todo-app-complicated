using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models
{
  public class FlowDTO
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public long Pos { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long CategoryId { get; set; }
  }
}
