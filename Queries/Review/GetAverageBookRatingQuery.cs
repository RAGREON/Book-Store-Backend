using MediatR;

namespace Store.Queries;

public record class GetAverageBookRatingQuery(int BookId) : IRequest<double>;
