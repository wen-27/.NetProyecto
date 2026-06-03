namespace Api.DTOs.VehicleEntryInventory;

// DTO usado para transportar datos de CreateVehicleEntryInventoryRequest entre la API y sus consumidores.
public sealed record CreateVehicleEntryInventoryRequest(
    int ServiceOrderId,
    bool HasScratches,
    string? ScratchesDescription,
    bool HasToolbox,
    string? ToolboxDescription,
    bool OwnershipCardDelivered,
    string? Observations);

// DTO usado para transportar datos de UpdateVehicleEntryInventoryRequest entre la API y sus consumidores.
public sealed record UpdateVehicleEntryInventoryRequest(
    bool HasScratches,
    string? ScratchesDescription,
    bool HasToolbox,
    string? ToolboxDescription,
    bool OwnershipCardDelivered,
    string? Observations);

// DTO usado para transportar datos de VehicleEntryInventoryResponse entre la API y sus consumidores.
public sealed record VehicleEntryInventoryResponse(
    int Id,
    int ServiceOrderId,
    bool HasScratches,
    string? ScratchesDescription,
    bool HasToolbox,
    string? ToolboxDescription,
    bool OwnershipCardDelivered,
    string? Observations,
    DateTime RegisteredAt);
