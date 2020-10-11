using AutoMapper;

namespace todoCore3.Api.Models
{
  public class TodoAppProfile : Profile
  {
    public TodoAppProfile()
    {
      CreateMap<TodoItem, TodoItemDto>();
      CreateMap<TodoItemDto, TodoItem>();
      CreateMap<Flow, FlowDto>();
      CreateMap<Category, CategoryDto>();
      CreateMap<CategoryDto, Category>();
      CreateMap<Category, CategoryWithItems>();
    }
  }
}
