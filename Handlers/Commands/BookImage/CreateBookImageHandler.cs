using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Api.DTOs;
using Store.Commands;
using Store.Data;
using Store.Api.Models;
using Store.Exceptions;

namespace Commands.Handlers;

public class CreateBookImageHandler : IRequestHandler<CreateBookImageCommand, BookImageDto>
{
  private readonly AppDbContext _context;

  public CreateBookImageHandler(AppDbContext context)
  {
    _context = context;
  }

  public async Task<BookImageDto> Handle(CreateBookImageCommand request, CancellationToken cancellationToken)
  {
    var book = await _context.Books
      .Include(b => b.Genres)
      .FirstOrDefaultAsync(b => b.Id == request.bookId, cancellationToken);

    if (book == null)
    {
      throw new NotFoundException(nameof(Book), request.bookId);
    }

    var dto = request.BookImageDto;

    var bookImage = new BookImage
    {
      BookId = request.bookId,
      Book = book,
      Type = dto.Type,
      Url = dto.Url
    };

    return new BookImageDto
    (
      bookImage.Id,
      bookImage.BookId,
      bookImage.Book,
      bookImage.Type,
      bookImage.Url
    );
  }
}