using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.UseCase.OrderServiceParts;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateOrderServicePart.
public sealed class CreateOrderServicePartHandler : IRequestHandler<CreateOrderServicePart, int>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IOrderServicePartRepository _orderServiceParts;
    private readonly IPartRepository _parts;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderServicePartHandler(IOrderServicePartRepository orderServiceParts, IPartRepository parts, IUnitOfWork unitOfWork)
    {
        _orderServiceParts = orderServiceParts;
        _parts = parts;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateOrderServicePart request, CancellationToken ct)
    {
        if (request.Quantity < 1)
        {
            throw new InvalidOperationException("La cantidad debe ser mayor que cero.");
        }

        var part = await _parts.GetByIdAsync(request.PartId, ct)
            ?? throw new KeyNotFoundException("No se encontró el repuesto.");

        if (!part.IsActive)
        {
            throw new InvalidOperationException("No se puede usar un repuesto inactivo.");
        }

        if (part.Stock < request.Quantity)
        {
            throw new InvalidOperationException("No hay stock suficiente para asignar el repuesto.");
        }

        if (await _orderServiceParts.ExistsOrderServiceAndPartAsync(request.OrderServiceId, request.PartId, ct))
        {
            throw new InvalidOperationException("Ese repuesto ya está asociado a la línea de servicio.");
        }

        part.Stock -= request.Quantity;

        var detail = new OrderServicePart
        {
            OrderServiceId = request.OrderServiceId,
            PartId = request.PartId,
            Quantity = request.Quantity,
            AppliedUnitPrice = request.AppliedUnitPrice,
            CustomerApproved = request.CustomerApproved,
            ApprovalDate = request.CustomerApproved.HasValue ? DateTime.UtcNow : null
        };

        await _parts.UpdateAsync(part, ct);
        await _orderServiceParts.AddAsync(detail, ct);
        await _unitOfWork.CommitAsync(ct);

        return detail.Id;
    }
}
