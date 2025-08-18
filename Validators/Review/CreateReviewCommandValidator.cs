using FluentValidation;
using Store.Commands;
using Store.Data;
using Microsoft.EntityFrameworkCore;

namespace Store.Validators;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
  private readonly AppDbContext _context;

  public CreateReviewCommandValidator(AppDbContext context)
  {
    _context = context;

    RuleFor(x => (int)x.ReviewDto.Rating)
      .InclusiveBetween(0, 5)
      .WithMessage("Rating must be in between 0 and 5.");

    RuleFor(x => x.ReviewDto.BookId)
      .GreaterThan(0)
      .WithMessage("Book Id must be greater than 0.");
  }
}