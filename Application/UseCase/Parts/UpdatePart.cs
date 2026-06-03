using MediatR;

namespace Application.UseCase.Parts;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdatePart.
public sealed record UpdatePart(
    int Id,
    int PartCategoryId,
    int? PartBrandId,
    string Code,
    string Description,
    int Stock,
    int MinimumStock,
    decimal UnitPrice,
    bool IsActive) : IRequest;
