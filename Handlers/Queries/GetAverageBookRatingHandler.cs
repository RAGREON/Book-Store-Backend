using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Api.DTOs;
using Store.Queries;
using Store.Data;
using Store.Api.Models;

namespace Queries.Handlers;

public class GetAverageBookRatingHandler : IRequestHandler <GetAverageBookRatingQuery, double>
{
  private readonly AppDbContext _context;

  public GetAverageBookRatingHandler(AppDbContext context)
  {
    _context = context;
  }

  public async Task<double> Handle(GetAverageBookRatingQuery request, CancellationToken cancellationToken)
  {
    var reviews = await _context.Reviews
      .Where(r => r.BookId == request.BookId)
      .ToListAsync(cancellationToken);

    double averageRating = reviews.Average(r => r.Rating);

    return averageRating;
  }
}