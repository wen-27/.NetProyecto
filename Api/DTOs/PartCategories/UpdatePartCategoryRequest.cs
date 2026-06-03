namespace Api.DTOs.PartCategories;

// DTO usado para transportar datos de UpdatePartCategoryRequest entre la API y sus consumidores.
public sealed record UpdatePartCategoryRequest(string Name);
