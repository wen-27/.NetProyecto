namespace Api.DTOs.StreetTypes;

// DTO usado para transportar datos de CreateStreetTypeRequest entre la API y sus consumidores.
public sealed record CreateStreetTypeRequest(string Name);
// DTO usado para transportar datos de UpdateStreetTypeRequest entre la API y sus consumidores.
public sealed record UpdateStreetTypeRequest(string Name);
// DTO usado para transportar datos de StreetTypeResponse entre la API y sus consumidores.
public sealed record StreetTypeResponse(int Id, string Name);
