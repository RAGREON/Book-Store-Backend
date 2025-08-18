using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Api.DTOs;
using Store.Commands;
using Store.Data;
using Store.Api.Models;
using Store.Exceptions;

namespace Commands.Handlers;

public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, ReviewDto>
{
  private readonly AppDbContext _context;

  public CreateReviewHandler(AppDbContext context)
  {
    _context = context;
  }
  
  public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
  {
    

    var dto = request.ReviewDto;
    var book = await _context.Books
      .AsNoTracking()
      .FirstOrDefaultAsync(b => b.Id == dto.BookId, cancellationToken);

    if (book is null)
    {
      throw new NotFoundException($"{nameof(Review)}/{nameof(Book)}", dto.BookId);
    }

    var review = new Review
    {
      Rating = dto.Rating,
      Description = dto.Description,
      BookId = dto.BookId,
    };

    _context.Reviews.Add(review);
    await _context.SaveChangesAsync(cancellationToken); 

    return new ReviewDto
    (
      review.Id,
      review.Rating,
      review.Description,
      review.BookId,
      review.ReviewDate
    );
  }
}