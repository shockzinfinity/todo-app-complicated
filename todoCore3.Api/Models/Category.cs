using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models
{
  public class Category
  {
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    [DefaultValue("rgb(0,121,191)")]
    public string BgColor { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
  }
}
