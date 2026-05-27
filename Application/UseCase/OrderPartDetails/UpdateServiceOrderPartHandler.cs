using Application.Abstractions;
using Domain.Enums.OrderStatus;
using Domain.ValueObjects.OrderPartDetail;
using MediatR;

namespace Application.UseCase.OrderPartDetails;

public sealed class UpdateServiceOrderPartHandler : IRequestHandler<UpdateServiceOrderPart>
{
    private readonly IOrderPartDetailRepository _orderPartDetails;
    private readonly IPartRepository _parts;
    private readonly IServiceOrderRepository _serviceOrders;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateServiceOrderPartHandler(
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

    public async Task Handle(UpdateServiceOrderPart request, CancellationToken ct)
    {
        var detail = await _orderPartDetails.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el repuesto de la orden.");

        var serviceOrder = await _serviceOrders.GetByIdAsync(detail.ServiceOrderId, ct)
            ?? throw new KeyNotFoundException("No se encontró la orden de servicio.");

        if (serviceOrder.OrderStatusId is (int)ServiceOrderStatus.Completed or (int)ServiceOrderStatus.Cancelled)
        {
            throw new InvalidOperationException("No se pueden modificar repuestos de una orden completada o cancelada.");
        }

        var quantity = new OrderPartDetailQuantity(request.Quantity);
        var appliedUnitPrice = new OrderPartDetailAppliedUnitPrice(request.AppliedUnitPrice);
        var part = await _parts.GetByIdAsync(detail.PartId, ct)
            ?? throw new KeyNotFoundException("No se encontró el repuesto.");

        var stockDelta = quantity.Value - detail.Quantity;
        if (stockDelta > 0 && part.Stock < stockDelta)
        {
            throw new InvalidOperationException("No hay stock suficiente para aumentar la cantidad del repuesto.");
        }

        part.Stock -= stockDelta;

        detail.Quantity = quantity.Value;
        detail.AppliedUnitPrice = appliedUnitPrice.Value;

        await _parts.UpdateAsync(part, ct);
        await _orderPartDetails.UpdateAsync(detail, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
