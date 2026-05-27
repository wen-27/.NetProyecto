namespace Api.DTOs.OrderStatusHistory;

public sealed record CreateOrderStatusHistoryRequest(
    int ServiceOrderId,
    int? PreviousOrderStatusId,
    int NewOrderStatusId,
    int ChangedByUserId,
    string? Observation);

public sealed record OrderStatusHistoryResponse(
    int Id,
    int ServiceOrderId,
    int? PreviousOrderStatusId,
    int NewOrderStatusId,
    int ChangedByUserId,
    string? Observation,
    DateTime ChangedAt);
