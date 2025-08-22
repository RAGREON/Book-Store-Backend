using Store.Api.DTOs;
using Store.Api.Models;

namespace Store.Api.DTOs;

public record class CreateBookImageDto
(
  ImageType Type,
  string Url 
);