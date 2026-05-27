namespace Api.DTOs.OrderPartDetails;

public sealed record UpdateServiceOrderPartRequest(int Quantity, decimal AppliedUnitPrice);
