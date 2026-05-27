namespace Api.DTOs.PartBrands;

public sealed record CreatePartBrandRequest(string Name);
public sealed record UpdatePartBrandRequest(string Name);
public sealed record PartBrandResponse(int Id, string Name);
