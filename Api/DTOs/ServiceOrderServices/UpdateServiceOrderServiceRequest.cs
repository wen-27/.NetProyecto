namespace Api.DTOs.ServiceOrderServices;

public sealed record UpdateServiceOrderServiceRequest(int ServiceTypeId, int MechanicId, string? Description, decimal LaborCost);
