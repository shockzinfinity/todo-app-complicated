using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todoCore3.Api.Models.Auth.Entities;

namespace todoCore3.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    public Account Account => (Account)HttpContext.Items["Account"];
    }
}