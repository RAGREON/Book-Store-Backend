using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Api.DTOs;
using Store.Queries;
using Store.Data;
using Store.Api.Models;

namespace Queries.Handlers;

public class GetGenresHandler : IRequestHandler<GetGenresQuery, List<GenreDto>>
{
  private readonly AppDbContext _context;

  public GetGenresHandler(AppDbContext context)
  {
    _context = context;
  }

  public async Task<List<GenreDto>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
  {
    var genres = await _context.Genres
      .Select(g => new GenreDto(g.Id, g.Name))
      .ToListAsync(cancellationToken);

    return genres;
  }
}