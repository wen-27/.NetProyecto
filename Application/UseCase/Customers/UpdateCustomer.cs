using MediatR;

namespace Application.UseCase.Customers;

public sealed record UpdateCustomer(int Id, bool Status) : IRequest;
