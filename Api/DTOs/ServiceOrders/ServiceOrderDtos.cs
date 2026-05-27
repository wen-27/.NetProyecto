namespace Api.DTOs.ServiceOrders;

public sealed record CreateServiceOrderRequest(
    int VehicleId,
    int OrderStatusId,
    DateTime? EstimatedDeliveryDate,
    string? GeneralDescription);

public sealed record UpdateServiceOrderRequest(
    int VehicleId,
    int OrderStatusId,
    DateTime? EstimatedDeliveryDate,
    string? GeneralDescription,
    string? CancellationReason,
    DateTime? CancellationDate);

public sealed record ServiceOrderResponse(
    int Id,
    int VehicleId,
    int OrderStatusId,
    DateTime EntryDate,
    DateTime? EstimatedDeliveryDate,
    string? GeneralDescription,
    string? CancellationReason,
    DateTime? CancellationDate,
    DateTime CreatedAt);
