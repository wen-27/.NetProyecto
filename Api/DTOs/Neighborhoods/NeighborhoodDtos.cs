namespace Api.DTOs.Neighborhoods;

// DTO usado para transportar datos de CreateNeighborhoodRequest entre la API y sus consumidores.
public sealed record CreateNeighborhoodRequest(int CityId, string Name);
// DTO usado para transportar datos de UpdateNeighborhoodRequest entre la API y sus consumidores.
public sealed record UpdateNeighborhoodRequest(int CityId, string Name);
// DTO usado para transportar datos de NeighborhoodResponse entre la API y sus consumidores.
public sealed record NeighborhoodResponse(int Id, int CityId, string Name);
