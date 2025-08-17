namespace Store.Api.DTOs;

public record class CreateBookDto
(
  string Name,
  DateOnly ReleaseDate,
  string Description,
  List<int> GenreIDs
);