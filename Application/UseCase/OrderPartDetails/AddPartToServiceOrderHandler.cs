using Application.Abstractions;
using Domain.Entities;
using Domain.Enums.OrderStatus;
using Domain.ValueObjects.OrderPartDetail;
using MediatR;

namespace Application.UseCase.OrderPartDetails;

public sealed class AddPartToServiceOrderHandler : IRequestHandler<AddPartToServiceOrder, int>
{
    private readonly IOrderPartDetailRepository _orderPartDetails;
    private readonly IPartRepository _parts;
    private readonly IServiceOrderRepository _serviceOrders;
    private readonly IUnitOfWork _unitOfWork;

    public AddPartToServiceOrderHandler(
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

    public async Task<int> Handle(AddPartToServiceOrder request, CancellationToken ct)
    {
        var serviceOrderId = new OrderPartDetailServiceOrderId(request.ServiceOrderId);
        var partId = new OrderPartDetailPartId(request.PartId);
        var quantity = new OrderPartDetailQuantity(request.Quantity);
        var appliedUnitPrice = new OrderPartDetailAppliedUnitPrice(request.AppliedUnitPrice);

        var serviceOrder = await _serviceOrders.GetByIdAsync(serviceOrderId.Value, ct)
            ?? throw new KeyNotFoundException("No se encontró la orden de servicio.");

        if (serviceOrder.OrderStatusId is (int)ServiceOrderStatus.Completed or (int)ServiceOrderStatus.Cancelled)
        {
            throw new InvalidOperationException("No se pueden agregar repuestos a una orden completada o cancelada.");
        }

        var part = await _parts.GetByIdAsync(partId.Value, ct)
            ?? throw new KeyNotFoundException("No se encontró el repuesto.");

        if (!part.IsActive)
        {
            throw new InvalidOperationException("No se puede usar un repuesto inactivo.");
        }

        if (part.Stock < quantity.Value)
        {
            throw new InvalidOperationException("No hay stock suficiente para asignar el repuesto a la orden.");
        }

        if (await _orderPartDetails.ExistsServiceOrderAndPartAsync(serviceOrderId, partId, ct))
        {
            throw new InvalidOperationException("Ese repuesto ya está asociado a la orden.");
        }

        part.Stock -= quantity.Value;

        var detail = new OrderPartDetail
        {
            ServiceOrderId = serviceOrderId.Value,
            PartId = partId.Value,
            Quantity = quantity.Value,
            AppliedUnitPrice = appliedUnitPrice.Value
        };

        await _parts.UpdateAsync(part, ct);
        await _orderPartDetails.AddAsync(detail, ct);
        await _unitOfWork.CommitAsync(ct);

        return detail.Id;
    }
}
