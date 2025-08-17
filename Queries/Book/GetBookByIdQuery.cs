using MediatR;
using Store.Api.DTOs;

namespace Store.Queries;

public record class GetBookByIdQuery(int Id) : IRequest<BookDto>;