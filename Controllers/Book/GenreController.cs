using Microsoft.AspNetCore.Mvc;
using MediatR;
using Store.Api.Models;
using Store.Api.DTOs;
using Store.Commands;
using Store.Queries;

namespace Store.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
  private readonly IMediator _mediator;

  public GenreController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpPost]
  public async Task<ActionResult<GenreDto>> CreateGenre([FromBody] CreateGenreDto dto)
  {
    var command = new CreateGenreCommand(dto);
    var result = await _mediator.Send(command);

    return CreatedAtAction
    (
      actionName: "GetGenre",
      routeValues: new { id = result.Id },
      value: result
    );
  }

  [HttpGet]
  public async Task<IActionResult> GetGenre()
  {
    return Ok();
  }


  [HttpGet]
  [Route("all")]
  public async Task<IActionResult> GetGenres()
  {
    var command = new GetGenresQuery();
    var result = await _mediator.Send(command);
    return Ok(result);
  }
}