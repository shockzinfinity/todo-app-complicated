using System;
using todoCore3.Api.Interfaces;
using todoCore3.Api.Models;

namespace todoCore3.Api.Handlers
{
  public class CreateTodoItemCommandHandler : ICreateTodoItemCommandHandler
  {
    public CreateTodoItemResponseModel CreateTodoItem(CreateTodoItemRequestModel requestModel)
    {
      var todoItem = new TodoItem
      {
        IsCompleted = requestModel.IsComplete,
        Name = requestModel.Name
      };

      
    }
  }
}
