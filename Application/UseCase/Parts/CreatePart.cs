using MediatR;

namespace Application.UseCase.Parts;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreatePart.
public sealed record CreatePart(
    int PartCategoryId,
    int? PartBrandId,
    string Code,
    string Description,
    int Stock,
    int MinimumStock,
    decimal UnitPrice) : IRequest<int>;
