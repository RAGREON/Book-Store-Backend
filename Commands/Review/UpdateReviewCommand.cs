using MediatR;
using Store.Api.DTOs;

namespace Store.Commands;

public record UpdateReviewCommand(int Id, UpdateReviewDto updateReviewDto) : IRequest<ReviewDto>;