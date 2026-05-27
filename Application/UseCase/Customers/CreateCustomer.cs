using MediatR;

namespace Application.UseCase.Customers;

public sealed record CreateCustomer(int PersonId) : IRequest<int>;
