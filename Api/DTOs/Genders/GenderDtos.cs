namespace Api.DTOs.Genders;

// DTO usado para transportar datos de CreateGenderRequest entre la API y sus consumidores.
public sealed record CreateGenderRequest(string Name);
// DTO usado para transportar datos de UpdateGenderRequest entre la API y sus consumidores.
public sealed record UpdateGenderRequest(string Name);
// DTO usado para transportar datos de GenderResponse entre la API y sus consumidores.
public sealed record GenderResponse(int Id, string Name);
