// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreatePartHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.Part;
using MediatR;

namespace Application.UseCase.Parts;

public sealed class CreatePartHandler : IRequestHandler<CreatePart, int>
{
    private readonly IPartRepository _parts;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePartHandler(IPartRepository parts, IUnitOfWork unitOfWork)
    {
        _parts = parts;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreatePart request, CancellationToken ct)
    {
        var categoryId = new PartCategoryId(request.PartCategoryId);
        var partBrandId = new PartBrandId(request.PartBrandId);
        var code = new PartCode(request.Code);
        var description = new PartDescription(request.Description);
        var stock = new PartStock(request.Stock);
        var minimumStock = new PartMinimumStock(request.MinimumStock);
        var unitPrice = new PartUnitPrice(request.UnitPrice);

        if (await _parts.ExistsCodeAsync(code, ct))
        {
            throw new InvalidOperationException("Ya existe un repuesto con ese código.");
        }

        var part = new Part
        {
            PartCategoryId = categoryId.Value,
            PartBrandId = partBrandId.Value,
            Code = code.Value,
            Description = description.Value,
            Stock = stock.Value,
            MinimumStock = minimumStock.Value,
            UnitPrice = unitPrice.Value
        };

        await _parts.AddAsync(part, ct);
        await _unitOfWork.CommitAsync(ct);

        return part.Id;
    }
}
