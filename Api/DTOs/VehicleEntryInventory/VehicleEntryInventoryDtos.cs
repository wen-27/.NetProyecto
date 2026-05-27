namespace Api.DTOs.VehicleEntryInventory;

public sealed record CreateVehicleEntryInventoryRequest(
    int ServiceOrderId,
    bool HasScratches,
    string? ScratchesDescription,
    bool HasToolbox,
    string? ToolboxDescription,
    bool OwnershipCardDelivered,
    string? Observations);

public sealed record UpdateVehicleEntryInventoryRequest(
    bool HasScratches,
    string? ScratchesDescription,
    bool HasToolbox,
    string? ToolboxDescription,
    bool OwnershipCardDelivered,
    string? Observations);

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
