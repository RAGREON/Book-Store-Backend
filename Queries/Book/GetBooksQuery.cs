using MediatR;
using Store.Api.DTOs;

namespace Store.Queries;

public record class GetBooksQuery() : IRequest<List<BookDto>>;