using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.Customer;
using MediatR;

namespace Application.UseCase.Customers;

public sealed class CreateCustomerHandler : IRequestHandler<CreateCustomer, int>
{
    private readonly ICustomerRepository _customers;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerHandler(ICustomerRepository customers, IUnitOfWork unitOfWork)
    {
        _customers = customers;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateCustomer request, CancellationToken ct)
    {
        var personId = new CustomerPersonId(request.PersonId);

        if (await _customers.ExistsPersonIdAsync(personId, ct))
        {
            throw new InvalidOperationException("Ya existe un cliente asociado a esa persona.");
        }

        var customer = new Customer
        {
            PersonId = personId.Value,
            Status = CustomerStatus.Active().IsActive
        };

        await _customers.AddAsync(customer, ct);
        await _unitOfWork.CommitAsync(ct);

        return customer.Id;
    }
}
