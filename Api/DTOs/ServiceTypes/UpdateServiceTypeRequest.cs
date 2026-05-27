namespace Api.DTOs.ServiceTypes;

public sealed record UpdateServiceTypeRequest(string Name, int EstimatedDays = 1);
