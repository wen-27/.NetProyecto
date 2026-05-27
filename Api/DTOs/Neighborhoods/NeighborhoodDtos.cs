namespace Api.DTOs.Neighborhoods;

public sealed record CreateNeighborhoodRequest(int CityId, string Name);
public sealed record UpdateNeighborhoodRequest(int CityId, string Name);
public sealed record NeighborhoodResponse(int Id, int CityId, string Name);
