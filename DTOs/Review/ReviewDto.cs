using Store.Api.Models;

namespace Store.Api.DTOs;

public record class ReviewDto
(
  int Id,
  short Rating,
  string? Description,
  int BookId,
  DateTime CreatedAt
);