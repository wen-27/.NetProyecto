namespace Api.DTOs.DocumentTypes;

// DTO usado para transportar datos de UpdateDocumentTypeRequest entre la API y sus consumidores.
public sealed record UpdateDocumentTypeRequest(string Code, string Name);
