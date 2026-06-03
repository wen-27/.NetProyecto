namespace Api.DTOs.PartBrands;

// DTO usado para transportar datos de CreatePartBrandRequest entre la API y sus consumidores.
public sealed record CreatePartBrandRequest(string Name);
// DTO usado para transportar datos de UpdatePartBrandRequest entre la API y sus consumidores.
public sealed record UpdatePartBrandRequest(string Name);
// DTO usado para transportar datos de PartBrandResponse entre la API y sus consumidores.
public sealed record PartBrandResponse(int Id, string Name);
