namespace Store.Api.DTOs;

public record class UpdateBookDto
(
  string Title, 
  string Author,
  string Description
);