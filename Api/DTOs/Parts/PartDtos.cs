namespace Api.DTOs.Parts;

public sealed record CreatePartRequest(
    int PartCategoryId,
    int? PartBrandId,
    string Code,
    string Description,
    int Stock,
    int MinimumStock,
    decimal UnitPrice,
    bool IsActive = true);

public sealed record PartResponse(
    int Id,
    int PartCategoryId,
    int? PartBrandId,
    string Code,
    string Description,
    int Stock,
    int MinimumStock,
    decimal UnitPrice,
    bool IsActive);
