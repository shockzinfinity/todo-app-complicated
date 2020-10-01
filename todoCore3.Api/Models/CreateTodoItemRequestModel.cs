using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todoCore3.Api.Models
{
  public class CreateTodoItemRequestModel
  {
    [Required]
    public string Name { get; set; }
    public bool IsComplete { get; set; }
  }
}
