using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todoCore3.Api.Models.Auth.Entities;

namespace todoCore3.Api.Controllers
{
  [Controller]
  public abstract class BaseController : ControllerBase
  {
    public Account Account => (Account)HttpContext.Items["Account"];
  }
}
