using Microsoft.AspNetCore.Mvc;
using MediatR;
using Store.Api.Models;
using Store.Api.DTOs;
using Store.Commands;

namespace Store.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
  private readonly IMediator _mediator;

  public BookController(IMediator mediator) 
  {
    _mediator = mediator;
  }

  [HttpPost]
  public async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookDto dto)
  {
    var command = new CreateBookCommand(dto); 
    var result = await _mediator.Send(command);
    return CreatedAtAction
    (
      actionName: "GetBook",
      routeValues: new { id = result.Id },
      value: result 
    );
  }
}