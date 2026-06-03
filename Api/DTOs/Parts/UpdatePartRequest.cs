namespace Api.DTOs.Parts;

// DTO usado para transportar datos de UpdatePartRequest entre la API y sus consumidores.
public sealed record UpdatePartRequest(
    int PartCategoryId,
    int? PartBrandId,
    string Code,
    string Description,
    int Stock,
    int MinimumStock,
    decimal UnitPrice,
    bool IsActive);
