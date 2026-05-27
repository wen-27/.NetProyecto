namespace Api.DTOs.ServiceTypes;

public sealed record CreateServiceTypeRequest(string Name, int EstimatedDays = 1);
public sealed record ServiceTypeResponse(int Id, string Name, int EstimatedDays);
