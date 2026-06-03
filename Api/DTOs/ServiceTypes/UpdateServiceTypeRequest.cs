namespace Api.DTOs.ServiceTypes;

// DTO usado para transportar datos de UpdateServiceTypeRequest entre la API y sus consumidores.
public sealed record UpdateServiceTypeRequest(string Name, int EstimatedDays = 1);
