using MediatR;
using Store.Api.DTOs;

namespace Store.Queries;

public record class GetReviewsByBookIdQuery(int BookId) : IRequest<List<ReviewDto>>;