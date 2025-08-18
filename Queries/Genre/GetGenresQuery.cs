using MediatR;
using Store.Api.DTOs;

namespace Store.Queries;

public record class GetGenresQuery() : IRequest<List<GenreDto>>;