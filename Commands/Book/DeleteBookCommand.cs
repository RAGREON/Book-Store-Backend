using Store.Api.DTOs;
using MediatR;

namespace Store.Commands;

public record DeleteBookCommand(int Id) : IRequest<bool>;