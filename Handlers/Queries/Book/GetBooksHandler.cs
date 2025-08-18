using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Api.DTOs;
using Store.Queries;
using Store.Data;
using Store.Api.Models;

namespace Queries.Handlers;

public class GetBooksHandler : IRequestHandler<GetBooksQuery, List<BookDto>>
{
  private readonly AppDbContext _context;

  public GetBooksHandler(AppDbContext context) 
  {
    _context = context;
  }

  public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
  {
    var books = await _context.Books
      .Select(b => new BookDto
      (
        b.Id,
        b.Name,
        b.ReleaseDate,
        b.Description,
        b.Genres.Select(g => new GenreDto(g.Id, g.Name)).ToList()
      ))
      .ToListAsync(cancellationToken);

    return books;
  }
}