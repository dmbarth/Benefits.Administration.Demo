using System;
using System.Threading.Tasks;
using Benefits.Administration.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Benefits.Administration.API.Middleware
{
  public class GlobalExceptionMiddleware
  {
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
      _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {        
        var response = context.Response;
        response.ContentType = "application/json";

        object json;

        switch (ex)
        {
          case NotFoundException e:
            response.StatusCode = StatusCodes.Status404NotFound;
            json = new { message = e.Text};
            break;
          default:
            response.StatusCode = StatusCodes.Status500InternalServerError;
            json = new { message = ex?.Message};
            break;
        }
        
        await response.WriteAsJsonAsync(json);
      }
    }
  }
}