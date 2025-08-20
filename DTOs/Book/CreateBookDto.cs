namespace Store.Api.DTOs;

public record class CreateBookDto
(
  string Title,
  string Author,
  DateOnly ReleaseDate,
  string Description,
  List<int> GenreIDs
);