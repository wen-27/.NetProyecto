namespace Api.DTOs.MechanicSpecialtyAssignments;

// DTO usado para transportar datos de CreateMechanicSpecialtyAssignmentRequest entre la API y sus consumidores.
public sealed record CreateMechanicSpecialtyAssignmentRequest(int PersonId, int SpecialtyId);
// DTO usado para transportar datos de UpdateMechanicSpecialtyAssignmentRequest entre la API y sus consumidores.
public sealed record UpdateMechanicSpecialtyAssignmentRequest(int PersonId, int SpecialtyId);
// DTO usado para transportar datos de MechanicSpecialtyAssignmentResponse entre la API y sus consumidores.
public sealed record MechanicSpecialtyAssignmentResponse(int Id, int PersonId, int SpecialtyId);
