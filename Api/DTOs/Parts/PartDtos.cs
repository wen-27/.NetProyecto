namespace Api.DTOs.Parts;

// DTO usado para transportar datos de CreatePartRequest entre la API y sus consumidores.
public sealed record CreatePartRequest(
    int PartCategoryId,
    int? PartBrandId,
    string Code,
    string Description,
    int Stock,
    int MinimumStock,
    decimal UnitPrice,
    bool IsActive = true);

// DTO usado para transportar datos de PartResponse entre la API y sus consumidores.
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
