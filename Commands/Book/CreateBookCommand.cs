using Store.Api.DTOs;
using MediatR;

namespace Store.Commands;

public record CreateBookCommand(CreateBookDto BookDto) : IRequest<BookDto>;