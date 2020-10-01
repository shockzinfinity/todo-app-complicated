using System;
using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models
{
  public class Category
  {
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
  }
}
