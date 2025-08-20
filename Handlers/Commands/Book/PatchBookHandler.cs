using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Api.DTOs;
using Store.Commands;
using Store.Data;
using Store.Exceptions;
using Store.Api.Models;

namespace Commands.Handlers;

public class PatchBookHandler : IRequestHandler<PatchBookCommand, BookDto>
{
  private readonly AppDbContext _context;

  public PatchBookHandler(AppDbContext context)
  {
    _context = context;
  }

  public async Task<BookDto> Handle(PatchBookCommand request, CancellationToken cancellationToken)
  {
    var book = await _context.Books
      .Include(b => b.Genres)
      .FirstOrDefaultAsync(b => b.Id == request.id, cancellationToken);

    if (book is null)
      throw new NotFoundException(nameof(Book), request.id);

    var bookDto = request.BookDto;
 
    book.Title = bookDto.Title?.Trim() ?? book.Title;
    book.Author = bookDto.Author?.Trim() ?? book.Author;
    book.Description = bookDto.Description?.Trim() ?? book.Description;

    await _context.SaveChangesAsync(cancellationToken);

    return MapToDto(book);
  }

  private BookDto MapToDto(Book book)
  {
    return new BookDto
    (
      book.Id,
      book.Title,
      book.Author,
      book.ReleaseDate,
      book.Description,
      book.Genres.Select(g => new GenreDto(g.Id, g.Name)).ToList()
    );
  }
}