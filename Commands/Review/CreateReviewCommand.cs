using MediatR;
using Store.Api.DTOs;

namespace Store.Commands;

public record class CreateReviewCommand(CreateReviewDto ReviewDto) : IRequest<ReviewDto>;