using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Store.Exceptions;

namespace Store.Middlewares;

public class ExceptionHandlingMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionHandlingMiddleware> _logger;

  public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger) 
  {
    _next = next;
    _logger = logger;
  } 

  public async Task Invoke(HttpContext context) 
  {
    try 
    {
      await _next(context);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "unhandled exception");

      await HandleExceptionAsync(context, ex);
    }
  }


  private static Task HandleExceptionAsync(HttpContext context, Exception ex)
  {
    var statusCode = StatusCodes.Status500InternalServerError;
    var title = "An unexpected error occurred";

    if (ex is NotFoundException) 
    {
      statusCode = StatusCodes.Status404NotFound;
      title = ex.Message;
    }

    var problemDetails = new ProblemDetails
    {
      Status = statusCode,
      Title = title,
      Detail = ex.InnerException?.Message
    };

    context.Response.ContentType = "application/json";
    context.Response.StatusCode = statusCode;

    return context.Response.WriteAsJsonAsync(problemDetails);
  }
}