namespace Api.DTOs.AuditActionTypes;

// DTO usado para transportar datos de CreateAuditActionTypeRequest entre la API y sus consumidores.
public sealed record CreateAuditActionTypeRequest(string Name);
// DTO usado para transportar datos de AuditActionTypeResponse entre la API y sus consumidores.
public sealed record AuditActionTypeResponse(int Id, string Name);
