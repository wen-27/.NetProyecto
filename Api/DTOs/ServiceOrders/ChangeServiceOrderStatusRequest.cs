namespace Api.DTOs.ServiceOrders;

public sealed record ChangeServiceOrderStatusRequest(int OrderStatusId, int UserId, string? Observation);
