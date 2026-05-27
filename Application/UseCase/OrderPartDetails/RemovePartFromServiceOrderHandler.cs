using Application.Abstractions;
using Domain.Enums.OrderStatus;
using MediatR;

namespace Application.UseCase.OrderPartDetails;

public sealed class RemovePartFromServiceOrderHandler : IRequestHandler<RemovePartFromServiceOrder>
{
    private readonly IOrderPartDetailRepository _orderPartDetails;
    private readonly IPartRepository _parts;
    private readonly IServiceOrderRepository _serviceOrders;
    private readonly IUnitOfWork _unitOfWork;

    public RemovePartFromServiceOrderHandler(
        IOrderPartDetailRepository orderPartDetails,
        IPartRepository parts,
        IServiceOrderRepository serviceOrders,
        IUnitOfWork unitOfWork)
    {
        _orderPartDetails = orderPartDetails;
        _parts = parts;
        _serviceOrders = serviceOrders;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemovePartFromServiceOrder request, CancellationToken ct)
    {
        var detail = await _orderPartDetails.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el repuesto de la orden.");

        var serviceOrder = await _serviceOrders.GetByIdAsync(detail.ServiceOrderId, ct)
            ?? throw new KeyNotFoundException("No se encontró la orden de servicio.");

        if (serviceOrder.OrderStatusId is (int)ServiceOrderStatus.Completed or (int)ServiceOrderStatus.Cancelled)
        {
            throw new InvalidOperationException("No se pueden quitar repuestos de una orden completada o cancelada.");
        }

        var part = await _parts.GetByIdAsync(detail.PartId, ct)
            ?? throw new KeyNotFoundException("No se encontró el repuesto.");

        part.Stock += detail.Quantity;

        await _parts.UpdateAsync(part, ct);
        await _orderPartDetails.RemoveAsync(detail, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
