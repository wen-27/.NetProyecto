namespace Api.DTOs.OrderStatusHistory;

// DTO usado para transportar datos de CreateOrderStatusHistoryRequest entre la API y sus consumidores.
public sealed record CreateOrderStatusHistoryRequest(
    int ServiceOrderId,
    int? PreviousOrderStatusId,
    int NewOrderStatusId,
    int ChangedByUserId,
    string? Observation);

// DTO usado para transportar datos de OrderStatusHistoryResponse entre la API y sus consumidores.
public sealed record OrderStatusHistoryResponse(
    int Id,
    int ServiceOrderId,
    int? PreviousOrderStatusId,
    int NewOrderStatusId,
    int ChangedByUserId,
    string? Observation,
    DateTime ChangedAt);
