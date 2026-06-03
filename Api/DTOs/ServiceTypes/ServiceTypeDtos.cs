namespace Api.DTOs.ServiceTypes;

// DTO usado para transportar datos de CreateServiceTypeRequest entre la API y sus consumidores.
public sealed record CreateServiceTypeRequest(string Name, int EstimatedDays = 1);
// DTO usado para transportar datos de ServiceTypeResponse entre la API y sus consumidores.
public sealed record ServiceTypeResponse(int Id, string Name, int EstimatedDays);
