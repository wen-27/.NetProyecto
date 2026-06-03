namespace Api.DTOs.OrderStatuses;

// DTO usado para transportar datos de UpdateOrderStatusRequest entre la API y sus consumidores.
public sealed record UpdateOrderStatusRequest(string Name);
