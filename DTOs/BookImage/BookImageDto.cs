using Store.Api.DTOs;
using Store.Api.Models;

namespace Store.Api.DTOs;

public record class BookImageDto
(
  int Id, 
  int BookId,
  Book? Book,
  ImageType Type,
  string Url
);