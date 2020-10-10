using AutoMapper;
using todoCore3.Api.Models;

namespace todoCore3.Api
{
  public class UserProfile : Profile
  {
    public UserProfile()
    {
      CreateMap<User, UserDTO>();
      CreateMap<UserRegisterDTO, User>();
      CreateMap<UserUpdateDTO, User>();
    }
  }
}
