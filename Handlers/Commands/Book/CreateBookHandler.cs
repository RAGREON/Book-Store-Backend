using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Api.DTOs;
using Store.Commands;
using Store.Data;
using Store.Api.Models;

namespace Commands.Handlers;

public class CreateBookHandler : IRequestHandler<CreateBookCommand, BookDto>
{ 
  private readonly AppDbContext _context;

  public CreateBookHandler(AppDbContext context) 
  {
    _context = context;
  }

  public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
  {
    var dto = request.BookDto;
    var genres = await _context.Genres
      .Where(g => dto.GenreIDs.Contains(g.Id))
      .ToListAsync(cancellationToken);

    var book = new Book
    {
      Name = dto.Name,
      ReleaseDate = dto.ReleaseDate,
      Description = dto.Description,
      Genres = genres
    };

    _context.Books.Add(book);
    await _context.SaveChangesAsync(cancellationToken);

    return new BookDto
    (
      book.Id,
      book.Name,
      book.ReleaseDate,
      book.Description,
      book.Genres.Select(g => new GenreDto(g.Id, g.Name)).ToList()
    );
  }
}