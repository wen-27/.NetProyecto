namespace Api.DTOs.Parts;

public sealed record UpdatePartRequest(
    int PartCategoryId,
    int? PartBrandId,
    string Code,
    string Description,
    int Stock,
    int MinimumStock,
    decimal UnitPrice,
    bool IsActive);
