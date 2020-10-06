using AutoMapper;

namespace todoCore3.Api.Models
{
  public class TodoItemProfile : Profile
  {
    public TodoItemProfile()
    {
      CreateMap<TodoItem, TodoItemDTO>();
    }
  }

  public class FlowProfile : Profile
  {
    public FlowProfile()
    {
      CreateMap<Flow, FlowDTO>();
    }
  }
}
