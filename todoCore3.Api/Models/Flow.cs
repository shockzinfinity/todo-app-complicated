using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models
{
  public class Flow
  {
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    [DefaultValue(65536)]
    public long Pos { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
    public long CategoryId { get; set; }
  }
}
