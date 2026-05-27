using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.ServiceOrderService;
using MediatR;

namespace Application.UseCase.ServiceOrderServices;

public sealed class AddServiceToOrderHandler : IRequestHandler<AddServiceToOrder, int>
{
    private readonly IServiceOrderRepository _serviceOrders;
    private readonly IServiceOrderServiceRepository _services;
    private readonly IUnitOfWork _unitOfWork;

    public AddServiceToOrderHandler(
        IServiceOrderRepository serviceOrders,
        IServiceOrderServiceRepository services,
        IUnitOfWork unitOfWork)
    {
        _serviceOrders = serviceOrders;
        _services = services;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(AddServiceToOrder request, CancellationToken ct)
    {
        var serviceOrderId = new ServiceOrderServiceOrderId(request.ServiceOrderId);
        var serviceTypeId = new ServiceOrderServiceTypeId(request.ServiceTypeId);
        var mechanicId = new ServiceOrderServiceMechanicId(request.MechanicId);
        var description = new ServiceOrderServiceDescription(request.Description);
        var laborCost = new ServiceOrderServiceLaborCost(request.LaborCost);

        _ = await _serviceOrders.GetByIdAsync(serviceOrderId.Value, ct)
            ?? throw new KeyNotFoundException("No se encontró la orden de servicio.");

        if (await _services.ExistsServiceOrderAndServiceTypeAsync(serviceOrderId.Value, serviceTypeId.Value, ct))
        {
            throw new InvalidOperationException("La orden ya tiene asignado ese tipo de servicio.");
        }

        var service = new ServiceOrderService
        {
            ServiceOrderId = serviceOrderId.Value,
            ServiceTypeId = serviceTypeId.Value,
            MechanicId = mechanicId.Value,
            Description = description.Value,
            LaborCost = laborCost.Value
        };

        await _services.AddAsync(service, ct);
        await _unitOfWork.CommitAsync(ct);

        return service.Id;
    }
}
