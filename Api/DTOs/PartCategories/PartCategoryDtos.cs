namespace Api.DTOs.PartCategories;

public sealed record CreatePartCategoryRequest(string Name);
public sealed record PartCategoryResponse(int Id, string Name);
