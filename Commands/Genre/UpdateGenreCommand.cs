using MediatR;
using Store.Api.DTOs;

namespace Store.Commands;

public record class UpdateGenreCommand(int Id, UpdateGenreCommand GenreDto) : IRequest<GenreDto>;