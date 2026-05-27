namespace Api.DTOs.MechanicAssignments;

public sealed record CreateMechanicAssignmentRequest(int OrderServiceId, int MechanicPersonId, int SpecialtyId);
public sealed record UpdateMechanicAssignmentRequest(int OrderServiceId, int MechanicPersonId, int SpecialtyId);
public sealed record MechanicAssignmentResponse(int Id, int OrderServiceId, int MechanicPersonId, int SpecialtyId);
