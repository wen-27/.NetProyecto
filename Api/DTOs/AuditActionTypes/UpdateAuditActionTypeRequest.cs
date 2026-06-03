namespace Api.DTOs.AuditActionTypes;

// DTO usado para transportar datos de UpdateAuditActionTypeRequest entre la API y sus consumidores.
public sealed record UpdateAuditActionTypeRequest(string Name);
