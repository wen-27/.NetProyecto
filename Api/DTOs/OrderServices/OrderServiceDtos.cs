namespace Api.DTOs.OrderServices;

public sealed record CreateOrderServiceRequest(
    int ServiceOrderId,
    int ServiceTypeId,
    string? Description,
    decimal LaborCost);

public sealed record UpdateOrderServiceRequest(
    int ServiceTypeId,
    string? Description,
    string? WorkPerformed,
    decimal LaborCost,
    bool? CustomerApproved,
    DateTime? ApprovalDate);

public sealed record OrderServiceResponse(
    int Id,
    int ServiceOrderId,
    int ServiceTypeId,
    string? Description,
    string? WorkPerformed,
    decimal LaborCost,
    bool? CustomerApproved,
    DateTime? ApprovalDate);
