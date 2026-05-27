namespace Api.DTOs.OrderServiceParts;

public sealed record CreateOrderServicePartRequest(
    int OrderServiceId,
    int PartId,
    int Quantity,
    decimal AppliedUnitPrice);

public sealed record UpdateOrderServicePartRequest(
    int Quantity,
    decimal AppliedUnitPrice,
    bool? CustomerApproved,
    DateTime? ApprovalDate);

public sealed record OrderServicePartResponse(
    int Id,
    int OrderServiceId,
    int PartId,
    int Quantity,
    decimal AppliedUnitPrice,
    bool? CustomerApproved,
    DateTime? ApprovalDate);
