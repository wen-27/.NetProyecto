namespace Api.DTOs.DocumentTypes;

// DTO usado para transportar datos de CreateDocumentTypeRequest entre la API y sus consumidores.
public sealed record CreateDocumentTypeRequest(string Code, string Name);
// DTO usado para transportar datos de DocumentTypeResponse entre la API y sus consumidores.
public sealed record DocumentTypeResponse(int Id, string Code, string Name);
