namespace Api.DTOs.ServiceOrders;

// DTO usado para transportar datos de ChangeServiceOrderStatusRequest entre la API y sus consumidores.
public sealed record ChangeServiceOrderStatusRequest(int OrderStatusId, int UserId, string? Observation);
