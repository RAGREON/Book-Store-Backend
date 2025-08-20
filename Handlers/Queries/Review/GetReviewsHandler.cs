using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Api.DTOs;
using Store.Queries;
using Store.Data;
using Store.Api.Models;

namespace Queries.Handlers;

public class GetReviewsHandler : IRequestHandler<GetReviewsQuery, List<ReviewDto>>
{
  private readonly AppDbContext _context;

  public GetReviewsHandler(AppDbContext context)
  {
    _context = context;
  }

  public async Task<List<ReviewDto>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
  {
    var reviews = await _context.Reviews
      .Select(r => new ReviewDto
      (
        r.Id,
        r.Rating,
        r.Description,
        r.BookId,
        r.CreatedAt,
        r.EditedAt
      )).ToListAsync(cancellationToken);

    return reviews;
  }
}