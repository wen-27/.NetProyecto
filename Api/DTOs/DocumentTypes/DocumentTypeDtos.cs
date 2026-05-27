namespace Api.DTOs.DocumentTypes;

public sealed record CreateDocumentTypeRequest(string Code, string Name);
public sealed record DocumentTypeResponse(int Id, string Code, string Name);
