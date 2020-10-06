using AutoMapper;

namespace todoCore3.Api.Models
{
  public class TodoAppProfile : Profile
  {
    public TodoAppProfile()
    {
      CreateMap<TodoItem, TodoItemDto>();
      CreateMap<Flow, FlowDto>();
      CreateMap<Category, CategoryDto>();
      CreateMap<Category, CategoryWithItems>();
    }
  }
}
