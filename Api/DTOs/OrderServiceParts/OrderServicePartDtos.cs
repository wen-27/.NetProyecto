namespace Api.DTOs.OrderServiceParts;

// DTO usado para transportar datos de CreateOrderServicePartRequest entre la API y sus consumidores.
public sealed record CreateOrderServicePartRequest(
    int OrderServiceId,
    int PartId,
    int Quantity,
    decimal AppliedUnitPrice);

// DTO usado para transportar datos de UpdateOrderServicePartRequest entre la API y sus consumidores.
public sealed record UpdateOrderServicePartRequest(
    int Quantity,
    decimal AppliedUnitPrice,
    bool? CustomerApproved,
    DateTime? ApprovalDate);

// DTO usado para transportar datos de OrderServicePartResponse entre la API y sus consumidores.
public sealed record OrderServicePartResponse(
    int Id,
    int OrderServiceId,
    int PartId,
    int Quantity,
    decimal AppliedUnitPrice,
    bool? CustomerApproved,
    DateTime? ApprovalDate);
