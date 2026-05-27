using MediatR;

namespace Application.UseCase.PersonDocuments;

public sealed record AddPersonDocument(int PersonId, int DocumentTypeId, string DocumentNumber, bool IsPrimary) : IRequest<int>;
