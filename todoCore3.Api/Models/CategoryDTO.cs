using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace todoCore3.Api.Models
{
  public class CategoryDTO
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public string BgColor { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
