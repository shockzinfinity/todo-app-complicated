using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace todoCore3.Api.Middleware
{
  public class ErrorHandlerMiddleware
  {
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
      }
      catch (Exception ex)
      {
        var response = context.Response;
        response.ContentType = "application/json";

        switch (ex)
        {
          case AppException e:
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            break;

          case KeyNotFoundException e:
            response.StatusCode = (int)HttpStatusCode.NotFound;
            break;

          default:
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            break;
        }

        var result = JsonSerializer.Serialize(new { message = ex?.Message });
        await response.WriteAsync(result);
      }
    }
  }
}
