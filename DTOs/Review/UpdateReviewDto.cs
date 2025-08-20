namespace Store.Api.DTOs;

public record class UpdateReviewDto
(
  short Rating,
  string Description 
);