namespace Api.DTOs.MechanicSpecialties;

// DTO usado para transportar datos de CreateMechanicSpecialtyRequest entre la API y sus consumidores.
public sealed record CreateMechanicSpecialtyRequest(string Name);
// DTO usado para transportar datos de UpdateMechanicSpecialtyRequest entre la API y sus consumidores.
public sealed record UpdateMechanicSpecialtyRequest(string Name);
// DTO usado para transportar datos de MechanicSpecialtyResponse entre la API y sus consumidores.
public sealed record MechanicSpecialtyResponse(int Id, string Name);
