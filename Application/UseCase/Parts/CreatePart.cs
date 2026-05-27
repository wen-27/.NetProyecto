using MediatR;

namespace Application.UseCase.Parts;

public sealed record CreatePart(
    int PartCategoryId,
    int? PartBrandId,
    string Code,
    string Description,
    int Stock,
    int MinimumStock,
    decimal UnitPrice) : IRequest<int>;
