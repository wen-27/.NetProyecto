namespace Api.DTOs.OrderStatuses;

public sealed record CreateOrderStatusRequest(string Name);
public sealed record OrderStatusResponse(int Id, string Name);
