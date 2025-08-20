using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Api.DTOs;
using Store.Commands;
using Store.Data;
using Store.Api.Models;
using Store.Exceptions;

namespace Commands.Handlers;

public class UpdateReviewHandler : IRequestHandler<UpdateReviewCommand, ReviewDto>
{
  private readonly AppDbContext _context;

  public UpdateReviewHandler(AppDbContext context)
  {
    _context = context;
  }
  
  public async Task<ReviewDto> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
  {
    var review = await _context.Reviews.FindAsync
      (new object[] { request.Id }, cancellationToken);
    
    if (review == null) 
    {
      throw new NotFoundException(nameof(Review), request.Id);
    }
    
    var dto = request.updateReviewDto;

    review.Rating = dto.Rating;
    review.Description = dto.Description;

    await _context.SaveChangesAsync(cancellationToken);

    return new ReviewDto
    (
      review.Id,
      review.Rating,
      review.Description,
      review.BookId,
      review.CreatedAt,
      review.EditedAt
    );
  }
}