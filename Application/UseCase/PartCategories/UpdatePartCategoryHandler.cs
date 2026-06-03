using Application.Abstractions;
using Domain.ValueObjects.PartCategory;
using MediatR;

namespace Application.UseCase.PartCategories;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdatePartCategory.
public sealed class UpdatePartCategoryHandler : IRequestHandler<UpdatePartCategory>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPartCategoryRepository _partCategories;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePartCategoryHandler(IPartCategoryRepository partCategories, IUnitOfWork unitOfWork)
    {
        _partCategories = partCategories;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdatePartCategory request, CancellationToken ct)
    {
        var partCategory = await _partCategories.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró la categoría de repuesto.");

        var name = new PartCategoryName(request.Name);
        partCategory.Name = name.Value;

        await _partCategories.UpdateAsync(partCategory, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
