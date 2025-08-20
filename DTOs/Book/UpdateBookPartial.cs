namespace Store.Api.DTOs;

public record class UpdateBookPartialDto
(
  string? Title, 
  string? Author,
  string? Description
);