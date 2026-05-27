using MediatR;

namespace Application.UseCase.Parts;

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
