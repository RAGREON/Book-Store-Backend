using Microsoft.AspNetCore.Mvc;
using MediatR;
using Store.Api.Models;
using Store.Api.DTOs;
using Store.Commands;
using Store.Queries;
using Microsoft.AspNetCore.JsonPatch;

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
      actionName: "GetBooks",
      routeValues: new { id = result.Id },
      value: result 
    );
  }

  [HttpGet]
  [Route("{id}")]
  public async Task<ActionResult<BookDto>> GetBookById([FromRoute] int id)
  {
    var command = new GetBookByIdQuery(id);
    var result = await _mediator.Send(command);
    return Ok(result);
  }

  [HttpGet]
  [Route("all")]
  public async Task<ActionResult<List<BookDto>>> GetBooks()
  {
    var command = new GetBooksQuery();
    var result = await _mediator.Send(command);
    return Ok(result);
  }

  [HttpPut]
  [Route("{id}")]
  public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] UpdateBookDto dto)
  {
    var command = new UpdateBookCommand(id, dto);
    var result = await _mediator.Send(command);
    return Ok(result);
  }

  [HttpPatch]
  [Route("{id}")]
  public async Task<IActionResult> PatchBook([FromRoute] int id, [FromBody]  UpdateBookPartialDto dto) 
  {
    var command = new PatchBookCommand(id, dto);
    var result = await _mediator.Send(command);
    return Ok(result);
  }
}