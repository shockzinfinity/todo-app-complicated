using AutoMapper;
using todoCore3.Api.Models.Auth;
using todoCore3.Api.Models.Auth.Entities;

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

      #region Accounts profiles

      CreateMap<Account, AccountResponse>();
      CreateMap<Account, AuthenticateResponse>();
      CreateMap<RegisterRequest, Account>();
      CreateMap<CreateAccountRequest, Account>();
      CreateMap<UpdateAccountRequest, Account>()
        .ForAllMembers(x => x.Condition(
          (src, dest, prop) =>
          {
            if (prop == null) return false;
            if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

            if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

            return true;
          }));

      #endregion Accounts profiles
    }
  }
}
