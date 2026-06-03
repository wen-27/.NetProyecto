namespace Api.DTOs.ServiceOrders;

// DTO usado para transportar datos de CreateServiceOrderRequest entre la API y sus consumidores.
public sealed record CreateServiceOrderRequest(
    int VehicleId,
    int OrderStatusId,
    DateTime? EstimatedDeliveryDate,
    string? GeneralDescription);

// DTO usado para transportar datos de UpdateServiceOrderRequest entre la API y sus consumidores.
public sealed record UpdateServiceOrderRequest(
    int VehicleId,
    int OrderStatusId,
    DateTime? EstimatedDeliveryDate,
    string? GeneralDescription,
    string? CancellationReason,
    DateTime? CancellationDate);

// DTO usado para transportar datos de ServiceOrderResponse entre la API y sus consumidores.
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

// DTO usado para transportar datos de CreateEmptyServiceOrderRequest entre la API y sus consumidores.
public sealed record CreateEmptyServiceOrderRequest(
    int ClientPersonId,
    int VehicleId);

// DTO usado para transportar datos de CreateDiagnosticServiceOrderRequest entre la API y sus consumidores.
public sealed record CreateDiagnosticServiceOrderRequest(
    int ClientPersonId,
    int VehicleId,
    DateTime EntryDate,
    int Mileage,
    string ProblemDescription,
    string Observations,
    DateTime EstimatedDeliveryDate,
    DiagnosticEntryChecklistRequest Checklist,
    DiagnosticServiceAssignmentRequest? ServiceAssignment,
    IReadOnlyList<DiagnosticServiceAssignmentRequest>? ServiceAssignments);

// DTO usado para transportar datos de DiagnosticEntryChecklistRequest entre la API y sus consumidores.
public sealed record DiagnosticEntryChecklistRequest(
    bool Lights,
    bool Tires,
    bool Mirrors,
    bool Documents,
    bool Tools,
    bool ScratchesOrDents,
    string FuelLevel,
    string ObjectsInsideVehicle,
    string? Notes);

// DTO usado para transportar datos de DiagnosticServiceAssignmentRequest entre la API y sus consumidores.
public sealed record DiagnosticServiceAssignmentRequest(
    int ServiceTypeId,
    int? WorkshopServiceId,
    int SpecialtyId,
    int MechanicPersonId,
    string Observation,
    decimal LaborCost);
