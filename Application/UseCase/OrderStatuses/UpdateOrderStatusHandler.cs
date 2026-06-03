using Application.Abstractions;
using Domain.ValueObjects.OrderStatus;
using MediatR;

namespace Application.UseCase.OrderStatuses;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateOrderStatus.
public sealed class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatus>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IOrderStatusRepository _orderStatuses;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderStatusHandler(IOrderStatusRepository orderStatuses, IUnitOfWork unitOfWork)
    {
        _orderStatuses = orderStatuses;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateOrderStatus request, CancellationToken ct)
    {
        var orderStatus = await _orderStatuses.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el estado de orden.");

        var name = new OrderStatusName(request.Name);
        orderStatus.Name = name.Value;

        await _orderStatuses.UpdateAsync(orderStatus, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
