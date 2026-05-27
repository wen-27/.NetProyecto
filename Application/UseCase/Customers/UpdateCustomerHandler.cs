using Application.Abstractions;
using Domain.ValueObjects.Customer;
using MediatR;

namespace Application.UseCase.Customers;

public sealed class UpdateCustomerHandler : IRequestHandler<UpdateCustomer>
{
    private readonly ICustomerRepository _customers;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomerHandler(ICustomerRepository customers, IUnitOfWork unitOfWork)
    {
        _customers = customers;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateCustomer request, CancellationToken ct)
    {
        var customer = await _customers.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el cliente.");

        var status = new CustomerStatus(request.Status);
        customer.Status = status.IsActive;

        await _customers.UpdateAsync(customer, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
