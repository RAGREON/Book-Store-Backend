using MediatR;
using Store.Api.DTOs;

namespace Store.Queries;

public record class GetReviewsQuery() : IRequest<List<ReviewDto>>;