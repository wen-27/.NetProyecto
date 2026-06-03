// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con UpdateOrderServicePartHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using MediatR;

namespace Application.UseCase.OrderServiceParts;

public sealed class UpdateOrderServicePartHandler : IRequestHandler<UpdateOrderServicePart>
{
    private readonly IOrderServicePartRepository _orderServiceParts;
    private readonly IPartRepository _parts;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderServicePartHandler(IOrderServicePartRepository orderServiceParts, IPartRepository parts, IUnitOfWork unitOfWork)
    {
        _orderServiceParts = orderServiceParts;
        _parts = parts;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateOrderServicePart request, CancellationToken ct)
    {
        if (request.Quantity < 1)
        {
            throw new InvalidOperationException("La cantidad debe ser mayor que cero.");
        }

        var detail = await _orderServiceParts.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el repuesto de la línea de servicio.");

        var part = await _parts.GetByIdAsync(detail.PartId, ct)
            ?? throw new KeyNotFoundException("No se encontró el repuesto.");

        var stockDelta = request.Quantity - detail.Quantity;
        if (stockDelta > 0 && part.Stock < stockDelta)
        {
            throw new InvalidOperationException("No hay stock suficiente para aumentar la cantidad del repuesto.");
        }

        part.Stock -= stockDelta;
        detail.Quantity = request.Quantity;
        detail.AppliedUnitPrice = request.AppliedUnitPrice;
        detail.CustomerApproved = request.CustomerApproved;
        detail.ApprovalDate = request.CustomerApproved.HasValue ? DateTime.UtcNow : null;

        await _parts.UpdateAsync(part, ct);
        await _orderServiceParts.UpdateAsync(detail, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
