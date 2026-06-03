// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con UpdateOrderStatusHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.ValueObjects.OrderStatus;
using MediatR;

namespace Application.UseCase.OrderStatuses;

public sealed class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatus>
{
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
