namespace Api.DTOs.AuditActionTypes;

public sealed record CreateAuditActionTypeRequest(string Name);
public sealed record AuditActionTypeResponse(int Id, string Name);
