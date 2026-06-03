namespace Api.DTOs.MechanicAssignments;

// DTO usado para transportar datos de CreateMechanicAssignmentRequest entre la API y sus consumidores.
public sealed record CreateMechanicAssignmentRequest(int OrderServiceId, int MechanicPersonId, int SpecialtyId);
// DTO usado para transportar datos de UpdateMechanicAssignmentRequest entre la API y sus consumidores.
public sealed record UpdateMechanicAssignmentRequest(int OrderServiceId, int MechanicPersonId, int SpecialtyId);
// DTO usado para transportar datos de MechanicAssignmentResponse entre la API y sus consumidores.
public sealed record MechanicAssignmentResponse(int Id, int OrderServiceId, int MechanicPersonId, int SpecialtyId);
