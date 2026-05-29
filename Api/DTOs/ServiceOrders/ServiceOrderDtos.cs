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

public sealed record CreateEmptyServiceOrderRequest(
    int ClientPersonId,
    int VehicleId);

public sealed record CreateDiagnosticServiceOrderRequest(
    int ClientPersonId,
    int VehicleId,
    DateTime EntryDate,
    int Mileage,
    string ProblemDescription,
    string Observations,
    DateTime EstimatedDeliveryDate,
    DiagnosticEntryChecklistRequest Checklist,
    DiagnosticServiceAssignmentRequest ServiceAssignment);

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

public sealed record DiagnosticServiceAssignmentRequest(
    int ServiceTypeId,
    int SpecialtyId,
    int MechanicPersonId,
    string Observation,
    decimal LaborCost);
