// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreateOrderStatusHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.OrderStatus;
using MediatR;

namespace Application.UseCase.OrderStatuses;

public sealed class CreateOrderStatusHandler : IRequestHandler<CreateOrderStatus, int>
{
    private readonly IOrderStatusRepository _orderStatuses;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderStatusHandler(IOrderStatusRepository orderStatuses, IUnitOfWork unitOfWork)
    {
        _orderStatuses = orderStatuses;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateOrderStatus request, CancellationToken ct)
    {
        var name = new OrderStatusName(request.Name);

        if (await _orderStatuses.ExistsNameAsync(name, ct))
        {
            throw new InvalidOperationException("Ya existe un estado de orden con ese nombre.");
        }

        var orderStatus = new OrderStatus { Name = name.Value };

        await _orderStatuses.AddAsync(orderStatus, ct);
        await _unitOfWork.CommitAsync(ct);

        return orderStatus.Id;
    }
}
