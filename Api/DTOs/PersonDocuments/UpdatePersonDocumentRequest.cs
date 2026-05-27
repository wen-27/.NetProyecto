namespace Api.DTOs.PersonDocuments;

public sealed record UpdatePersonDocumentRequest(int DocumentTypeId, string DocumentNumber, bool IsPrimary);
