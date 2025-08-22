using MediatR;
using Store.Api.DTOs;

namespace Store.Commands;

public record class CreateBookImageCommand(int bookId, CreateBookImageDto BookImageDto) : IRequest<BookImageDto>;