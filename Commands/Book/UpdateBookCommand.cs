using Store.Api.Models;
using Store.Api.DTOs;
using MediatR;

namespace Store.Commands;

public record UpdateBookCommand(int Id, UpdateBookDto BookDto) : IRequest<BookDto>;