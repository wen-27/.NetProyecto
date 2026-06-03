// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de VehicleEntryInventoryDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
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
