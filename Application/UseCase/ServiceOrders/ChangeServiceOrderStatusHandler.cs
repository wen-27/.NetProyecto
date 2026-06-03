// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con ChangeServiceOrderStatusHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.Enums.OrderStatus;
using Domain.ValueObjects.ServiceOrder;
using MediatR;

namespace Application.UseCase.ServiceOrders;

public sealed class ChangeServiceOrderStatusHandler : IRequestHandler<ChangeServiceOrderStatus>
{
    private readonly IServiceOrderRepository _serviceOrders;
    private readonly IOrderStatusHistoryRepository _statusHistory;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeServiceOrderStatusHandler(
        IServiceOrderRepository serviceOrders,
        IOrderStatusHistoryRepository statusHistory,
        IUnitOfWork unitOfWork)
    {
        _serviceOrders = serviceOrders;
        _statusHistory = statusHistory;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ChangeServiceOrderStatus request, CancellationToken ct)
    {
        var serviceOrder = await _serviceOrders.GetByIdAsync(request.ServiceOrderId, ct)
            ?? throw new KeyNotFoundException("No se encontró la orden de servicio.");

        var statusId = new ServiceOrderStatusId(request.OrderStatusId);

        if (!CanTransition(serviceOrder.OrderStatusId, statusId.Value))
        {
            throw new InvalidOperationException("La transición de estado de la orden no es válida.");
        }

        var previousStatusId = serviceOrder.OrderStatusId;
        serviceOrder.OrderStatusId = statusId.Value;

        await _statusHistory.AddAsync(new Domain.Entities.OrderStatusHistory
        {
            ServiceOrderId = serviceOrder.Id,
            PreviousOrderStatusId = previousStatusId,
            NewOrderStatusId = statusId.Value,
            UserId = request.UserId,
            ChangeDate = DateTime.UtcNow,
            Observation = request.Observation
        }, ct);

        await _serviceOrders.UpdateAsync(serviceOrder, ct);
        await _unitOfWork.CommitAsync(ct);
    }

    private static bool CanTransition(int currentStatusId, int nextStatusId)
    {
        if (currentStatusId == nextStatusId)
        {
            return true;
        }

        return (ServiceOrderStatus)currentStatusId switch
        {
            ServiceOrderStatus.Pending => nextStatusId is (int)ServiceOrderStatus.InProgress or (int)ServiceOrderStatus.Cancelled,
            ServiceOrderStatus.InProgress => nextStatusId is (int)ServiceOrderStatus.Completed or (int)ServiceOrderStatus.Cancelled,
            ServiceOrderStatus.Completed => false,
            ServiceOrderStatus.Cancelled => false,
            _ => false
        };
    }
}
