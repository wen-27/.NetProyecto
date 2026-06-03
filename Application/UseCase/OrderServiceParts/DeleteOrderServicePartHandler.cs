// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con DeleteOrderServicePartHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using MediatR;

namespace Application.UseCase.OrderServiceParts;

public sealed class DeleteOrderServicePartHandler : IRequestHandler<DeleteOrderServicePart>
{
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
