using Application.Abstractions;
using Domain.Entities;
using Domain.Enums.OrderStatus;
using Domain.ValueObjects.ServiceOrder;
using MediatR;

namespace Application.UseCase.ServiceOrders;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateServiceOrder.
public sealed class CreateServiceOrderHandler : IRequestHandler<CreateServiceOrder, int>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IServiceOrderRepository _serviceOrders;
    private readonly IUnitOfWork _unitOfWork;

    public CreateServiceOrderHandler(IServiceOrderRepository serviceOrders, IUnitOfWork unitOfWork)
    {
        _serviceOrders = serviceOrders;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateServiceOrder request, CancellationToken ct)
    {
        var vehicleId = new ServiceOrderVehicleId(request.VehicleId);
        var statusId = new ServiceOrderStatusId(request.OrderStatusId);
        var entryDate = new ServiceOrderEntryDate(DateTime.UtcNow);
        var estimatedDeliveryDate = new ServiceOrderEstimatedDeliveryDate(request.EstimatedDeliveryDate);

        if (statusId.Value != (int)ServiceOrderStatus.Pending)
        {
            throw new InvalidOperationException("Una orden nueva debe iniciar en estado pendiente.");
        }

        if (await _serviceOrders.HasActiveOrderForVehicleAsync(vehicleId, ct))
        {
            throw new InvalidOperationException("El vehículo ya tiene una orden de servicio activa.");
        }

        if (estimatedDeliveryDate.Value.HasValue && estimatedDeliveryDate.Value.Value <= entryDate.Value)
        {
            throw new InvalidOperationException("La fecha estimada de entrega debe ser posterior a la fecha de ingreso.");
        }

        var serviceOrder = new ServiceOrder
        {
            VehicleId = vehicleId.Value,
            OrderStatusId = statusId.Value,
            EntryDate = entryDate.Value,
            EstimatedDeliveryDate = estimatedDeliveryDate.Value
        };

        await _serviceOrders.AddAsync(serviceOrder, ct);
        await _unitOfWork.CommitAsync(ct);

        return serviceOrder.Id;
    }
}
