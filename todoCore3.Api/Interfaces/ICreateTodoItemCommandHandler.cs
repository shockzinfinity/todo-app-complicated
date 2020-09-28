using todoCore3.Api.Models;

namespace todoCore3.Api.Interfaces
{
  public interface ICreateTodoItemCommandHandler
  {
    CreateTodoItemResponseModel CreateTodoItem(CreateTodoItemRequestModel requestModel);
  }
}
