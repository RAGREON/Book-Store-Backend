namespace Store.Api.DTOs;

public record class CreateReviewDto
(
  short Rating,
  string Description,
  int BookId
);