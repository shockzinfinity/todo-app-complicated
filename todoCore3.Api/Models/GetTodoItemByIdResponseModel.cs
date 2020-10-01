using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todoCore3.Api.Models
{
  public class GetTodoItemByIdResponseModel
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsComplete { get; set; }
  }
}
