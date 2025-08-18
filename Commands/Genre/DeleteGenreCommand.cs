using MediatR;
using Store.Api.DTOs;

namespace Store.Commands;

public record class DeleteGenreCommand(int Id) : IRequest<bool>;