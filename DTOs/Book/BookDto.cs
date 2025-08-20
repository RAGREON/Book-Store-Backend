using Store.Api.Models;

namespace Store.Api.DTOs;

public record class BookDto 
(
  int Id,
  string Title,
  string Author,
  DateOnly ReleaseDate,
  string Description,
  List<GenreDto> Genres
);