using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.PartCategory;
using MediatR;

namespace Application.UseCase.PartCategories;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreatePartCategory.
public sealed class CreatePartCategoryHandler : IRequestHandler<CreatePartCategory, int>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IPartCategoryRepository _partCategories;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePartCategoryHandler(IPartCategoryRepository partCategories, IUnitOfWork unitOfWork)
    {
        _partCategories = partCategories;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreatePartCategory request, CancellationToken ct)
    {
        var name = new PartCategoryName(request.Name);

        if (await _partCategories.ExistsNameAsync(name, ct))
        {
            throw new InvalidOperationException("Ya existe una categoría de repuesto con ese nombre.");
        }

        var partCategory = new PartCategory { Name = name.Value };

        await _partCategories.AddAsync(partCategory, ct);
        await _unitOfWork.CommitAsync(ct);

        return partCategory.Id;
    }
}
