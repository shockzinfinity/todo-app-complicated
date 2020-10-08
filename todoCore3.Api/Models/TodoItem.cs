using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models
{
  public class TodoItem
  {
    public long Id { get; set; }
    public long FlowId { get; set; }

    [DefaultValue(65536)]
    public long Pos { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [DefaultValue(false)]
    public bool IsCompleted { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
