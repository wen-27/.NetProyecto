namespace Api.DTOs.MechanicSpecialtyAssignments;

public sealed record CreateMechanicSpecialtyAssignmentRequest(int PersonId, int SpecialtyId);
public sealed record UpdateMechanicSpecialtyAssignmentRequest(int PersonId, int SpecialtyId);
public sealed record MechanicSpecialtyAssignmentResponse(int Id, int PersonId, int SpecialtyId);
