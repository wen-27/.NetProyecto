namespace Api.DTOs.PartCategories;

// DTO usado para transportar datos de CreatePartCategoryRequest entre la API y sus consumidores.
public sealed record CreatePartCategoryRequest(string Name);
// DTO usado para transportar datos de PartCategoryResponse entre la API y sus consumidores.
public sealed record PartCategoryResponse(int Id, string Name);
