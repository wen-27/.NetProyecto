using MediatR;

namespace Application.UseCase.PersonDocuments;

public sealed record UpdatePersonDocument(int Id, int DocumentTypeId, string DocumentNumber, bool IsPrimary) : IRequest;
