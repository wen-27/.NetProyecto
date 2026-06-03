using Application.Abstractions;
using MediatR;

namespace Application.UseCase.OrderServiceParts;

// Caso de uso que modela una accion o consulta de negocio relacionada con DeleteOrderServicePart.
public sealed class DeleteOrderServicePartHandler : IRequestHandler<DeleteOrderServicePart>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IOrderServicePartRepository _orderServiceParts;
    private readonly IPartRepository _parts;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderServicePartHandler(IOrderServicePartRepository orderServiceParts, IPartRepository parts, IUnitOfWork unitOfWork)
    {
        _orderServiceParts = orderServiceParts;
        _parts = parts;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteOrderServicePart request, CancellationToken ct)
    {
        var detail = await _orderServiceParts.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el repuesto de la línea de servicio.");

        var part = await _parts.GetByIdAsync(detail.PartId, ct)
            ?? throw new KeyNotFoundException("No se encontró el repuesto.");

        part.Stock += detail.Quantity;

        await _parts.UpdateAsync(part, ct);
        await _orderServiceParts.RemoveAsync(detail, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
