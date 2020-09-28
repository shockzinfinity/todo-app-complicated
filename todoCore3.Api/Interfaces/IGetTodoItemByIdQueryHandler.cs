using todoCore3.Api.Models;

namespace todoCore3.Api.Interfaces
{
  public interface IGetTodoItemByIdQueryHandler
  {
    GetTodoItemByIdResponseModel GetTodoItemById(GetTodoItemByIdRequestModel requestModel);
  }
}
