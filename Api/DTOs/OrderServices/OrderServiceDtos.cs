namespace Api.DTOs.OrderServices;

// DTO usado para transportar datos de CreateOrderServiceRequest entre la API y sus consumidores.
public sealed record CreateOrderServiceRequest(
    int ServiceOrderId,
    int ServiceTypeId,
    string? Description,
    decimal LaborCost);

// DTO usado para transportar datos de UpdateOrderServiceRequest entre la API y sus consumidores.
public sealed record UpdateOrderServiceRequest(
    int ServiceTypeId,
    string? Description,
    string? WorkPerformed,
    decimal LaborCost,
    bool? CustomerApproved,
    DateTime? ApprovalDate);

// DTO usado para transportar datos de OrderServiceResponse entre la API y sus consumidores.
public sealed record OrderServiceResponse(
    int Id,
    int ServiceOrderId,
    int ServiceTypeId,
    int? WorkshopServiceId,
    string? Description,
    string? WorkPerformed,
    decimal LaborCost,
    decimal Price,
    int Status,
    bool? CustomerApproved,
    DateTime? ApprovalDate);
