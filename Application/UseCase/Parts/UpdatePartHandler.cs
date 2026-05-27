using Application.Abstractions;
using Domain.ValueObjects.Part;
using MediatR;

namespace Application.UseCase.Parts;

public sealed class UpdatePartHandler : IRequestHandler<UpdatePart>
{
    private readonly IPartRepository _parts;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePartHandler(IPartRepository parts, IUnitOfWork unitOfWork)
    {
        _parts = parts;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdatePart request, CancellationToken ct)
    {
        var part = await _parts.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el repuesto.");

        var categoryId = new PartCategoryId(request.PartCategoryId);
        var partBrandId = new PartBrandId(request.PartBrandId);
        var code = new PartCode(request.Code);
        var description = new PartDescription(request.Description);
        var stock = new PartStock(request.Stock);
        var minimumStock = new PartMinimumStock(request.MinimumStock);
        var unitPrice = new PartUnitPrice(request.UnitPrice);
        var isActive = new PartIsActive(request.IsActive);

        part.PartCategoryId = categoryId.Value;
        part.PartBrandId = partBrandId.Value;
        part.Code = code.Value;
        part.Description = description.Value;
        part.Stock = stock.Value;
        part.MinimumStock = minimumStock.Value;
        part.UnitPrice = unitPrice.Value;
        part.IsActive = isActive.Value;

        await _parts.UpdateAsync(part, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
