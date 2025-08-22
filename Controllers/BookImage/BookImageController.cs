using Microsoft.AspNetCore.Mvc;
using MediatR;
using Store.Api.Models;
using Store.Api.DTOs;
using Store.Commands;

namespace Store.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookImageController : ControllerBase
{
  private readonly IMediator _mediator;

  public BookImageController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpGet]
  [Route("{id}")]
  public async Task<IActionResult> GetBookImage([FromRoute] int id)
  {
    return Ok(id);
  }

  [HttpPost]
  [Route("{id}")]
  public async Task<IActionResult> UploadBookImage([FromRoute] int id, [FromBody] CreateBookImageDto dto)
  {
    var command = new CreateBookImageCommand(id, dto);
    var result = await _mediator.Send(command);
    return Ok(result);
  }
}