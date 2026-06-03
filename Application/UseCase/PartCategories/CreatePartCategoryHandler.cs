// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreatePartCategoryHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.PartCategory;
using MediatR;

namespace Application.UseCase.PartCategories;

public sealed class CreatePartCategoryHandler : IRequestHandler<CreatePartCategory, int>
{
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
