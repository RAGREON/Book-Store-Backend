using MediatR;
using Store.Api.DTOs;
using Store.Commands;
using Store.Data;
using Store.Api.Models;

namespace Commands.Handlers;

public class CreateGenreHandler : IRequestHandler<CreateGenreCommand, GenreDto>
{
  private readonly AppDbContext _context;

  public CreateGenreHandler(AppDbContext context)
  {
    _context = context;
  }

  public async Task<GenreDto> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
  {
    var dto = request.GenreDto;

    var genre = new Genre
    {
      Name = dto.Name
    };

    _context.Genres.Add(genre);
    _context.SaveChangesAsync(cancellationToken);

    return new GenreDto
    (
      genre.Id,
      genre.Name
    );
  }
}