using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using todoCore3.Api.Models;

namespace todoCore3.Api.Middleware
{
  public class JwtMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
      _next = next;
      _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, TodoContext todoContext)
    {
      // http context 상의 Authorization 헤더 검증
      var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last(); // Bearer 토큰에서 토큰만 추출

      if (token != null)
        await attachAccountToContext(context, todoContext, token);

      await _next(context);
    }

    private async Task attachAccountToContext(HttpContext context, TodoContext todoContext, string token)
    {
      try
      {
        // 토큰 검증
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false,
          // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
          ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        var accountId = int.Parse(jwtToken.Claims.First(c => c.Type == "id").Value);

        // attach
        context.Items["Account"] = await todoContext.Accounts.FindAsync(accountId);
      }
      catch { /* do nothing */ }
    }
  }
}
