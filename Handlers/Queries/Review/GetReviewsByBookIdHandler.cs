using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Api.DTOs;
using Store.Queries;
using Store.Data;
using Store.Api.Models;

namespace Queries.Handlers;

public class GetReviewsByBookIdHandler : IRequestHandler<GetReviewsByBookIdQuery, List<ReviewDto>>
{
  private readonly AppDbContext _context;

  public GetReviewsByBookIdHandler(AppDbContext context)
  {
    _context = context;
  }

  public async Task<List<ReviewDto>> Handle(GetReviewsByBookIdQuery request, CancellationToken cancellationToken)
  {
    var reviews = await _context.Reviews
      .Where(r => r.BookId == request.BookId)
      .Select(r => new ReviewDto
      (
        r.Id,
        r.Rating,
        r.Description,
        r.BookId,
        r.CreatedAt,
        r.EditedAt
      ))
      .ToListAsync(cancellationToken);

    return reviews;
  }
}