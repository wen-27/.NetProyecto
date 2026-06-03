using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.OrderStatus;
using MediatR;

namespace Application.UseCase.OrderStatuses;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateOrderStatus.
public sealed class CreateOrderStatusHandler : IRequestHandler<CreateOrderStatus, int>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
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
