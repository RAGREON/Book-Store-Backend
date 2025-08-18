using MediatR;
using Store.Api.DTOs;

namespace Store.Commands;

public record class CreateGenreCommand(CreateGenreDto GenreDto) : IRequest<GenreDto>;