using Microsoft.AspNetCore.Mvc;
using MediatR;
using Store.Api.Models;
using Store.Api.DTOs;
using Store.Commands;
using Store.Queries;

namespace Store.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
  private readonly IMediator _mediator;

  public ReviewController(IMediator mediator)
  {
    _mediator = mediator;
  }


  [HttpPost]
  public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto dto)
  {
    var command = new CreateReviewCommand(dto);
    var result = await _mediator.Send(command);
    return Ok(result);
  }

  [HttpGet]
  [Route("all")]
  public async Task<IActionResult> GetReviews() 
  {
    var command = new GetReviewsQuery();
    var result = await _mediator.Send(command);
    return Ok(result);
  }
}