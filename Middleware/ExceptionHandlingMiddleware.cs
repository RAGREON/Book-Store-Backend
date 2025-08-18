using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Store.Exceptions;
using FluentValidation;

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
    object? errors = null;

    if (ex is NotFoundException) 
    {
      statusCode = StatusCodes.Status404NotFound;
      title = ex.Message;
    }
    else if (ex is ValidationException validationEx)
    {
      statusCode = StatusCodes.Status400BadRequest;
      title = "Validation Failed";
      errors = validationEx.Errors.Select(e => new
      {
        e.PropertyName,
        e.ErrorMessage
      });
    }

    var problemDetails = new ProblemDetails
    {
      Status = statusCode,
      Title = title,
      Detail = ex.InnerException?.Message
    };

    context.Response.ContentType = "application/json";
    context.Response.StatusCode = statusCode;

    if (errors != null) 
    {
      return context.Response.WriteAsJsonAsync(new
      {
        problemDetails.Title,
        problemDetails.Status,
        Errors = errors
      });
    }

    return context.Response.WriteAsJsonAsync(problemDetails);
  }
}