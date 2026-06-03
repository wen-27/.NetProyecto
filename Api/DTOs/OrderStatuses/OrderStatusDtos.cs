namespace Api.DTOs.OrderStatuses;

// DTO usado para transportar datos de CreateOrderStatusRequest entre la API y sus consumidores.
public sealed record CreateOrderStatusRequest(string Name);
// DTO usado para transportar datos de OrderStatusResponse entre la API y sus consumidores.
public sealed record OrderStatusResponse(int Id, string Name);
