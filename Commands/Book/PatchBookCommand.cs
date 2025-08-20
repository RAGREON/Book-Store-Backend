using MediatR;
using Store.Api.DTOs;

namespace Store.Commands;

public record class PatchBookCommand(int id, UpdateBookPartialDto BookDto) : IRequest<BookDto>;