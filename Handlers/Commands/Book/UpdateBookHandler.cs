using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Api.DTOs;
using Store.Commands;
using Store.Data;
using Store.Exceptions;
using Store.Api.Models;

namespace Commands.Handlers;

public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, BookDto>
{
  private readonly AppDbContext _context;

  public UpdateBookHandler(AppDbContext context)
  {
    _context = context;
  }

  public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
  {
    var book = await _context.Books.FindAsync(new object[] { request.Id }, cancellationToken);

    if (book == null)
    {
      throw new NotFoundException(nameof(Book), request.Id);
    }

    var dto = request.BookDto;

    book.Title = dto.Title;
    book.Author = dto.Author;
    book.Description = dto.Description;

    await _context.SaveChangesAsync(cancellationToken);

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