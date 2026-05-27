namespace Api.DTOs.StreetTypes;

public sealed record CreateStreetTypeRequest(string Name);
public sealed record UpdateStreetTypeRequest(string Name);
public sealed record StreetTypeResponse(int Id, string Name);
