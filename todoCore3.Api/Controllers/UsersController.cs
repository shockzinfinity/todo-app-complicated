using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using todoCore3.Api.Models;
using todoCore3.Api.Services;

namespace todoCore3.Api.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private IUserService _userService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
    {
      _userService = userService ?? throw new ArgumentNullException(nameof(userService));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] UserAuthenticateDTO model)
    {
      var user = _userService.Authenticate(model.Username, model.Password);

      if (user == null)
        return BadRequest(new { message = "Username or Password is incorrect" });

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Name, user.Id.ToString())
        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      return Ok(new
      {
        Id = user.Id,
        Username = user.Username,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Token = tokenString
      });
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRegisterDTO model)
    {
      var user = _mapper.Map<User>(model);

      try
      {
        _userService.Create(user, model.Password);
        return Ok();
      }
      catch (AppException ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
      var users = _userService.GetAll();
      var model = _mapper.Map<IList<UserDTO>>(users);

      return Ok(model);
    }

    [HttpGet("{id}")]
    public IActionResult GetBy(int id)
    {
      var user = _userService.GetBy(id);
      var model = _mapper.Map<UserDTO>(user);

      return Ok(model);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UserUpdateDTO model)
    {
      var user = _mapper.Map<User>(model);
      user.Id = id;

      try
      {
        _userService.Update(user, model.Password);
        return Ok();
      }
      catch (AppException ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      _userService.Delete(id);

      return Ok();
    }
  }
}
