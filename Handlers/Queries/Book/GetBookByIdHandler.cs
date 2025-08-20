using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Api.DTOs;
using Store.Queries;
using Store.Data;
using Store.Api.Models;
using Store.Exceptions;

namespace Queries.Handlers;

public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, BookDto>
{
  private readonly AppDbContext _context;

  public GetBookByIdHandler(AppDbContext context)
  {
    _context = context;
  }

  public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
  {
    var book = await _context.Books
      .Include(b => b.Genres)
      .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);    

    if (book is null) {
      throw new NotFoundException("Book", request.Id);
    }

    return new BookDto(
      book.Id,
      book.Title,
      book.Author,
      book.ReleaseDate,
      book.Description,
      book.Genres.Select(g => new GenreDto(g.Id, g.Name)).ToList()
    );
  }
}