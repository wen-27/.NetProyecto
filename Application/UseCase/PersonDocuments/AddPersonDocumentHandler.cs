using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.PersonDocument;
using MediatR;

namespace Application.UseCase.PersonDocuments;

public sealed class AddPersonDocumentHandler : IRequestHandler<AddPersonDocument, int>
{
    private readonly IPersonDocumentRepository _personDocuments;
    private readonly IUnitOfWork _unitOfWork;

    public AddPersonDocumentHandler(IPersonDocumentRepository personDocuments, IUnitOfWork unitOfWork)
    {
        _personDocuments = personDocuments;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(AddPersonDocument request, CancellationToken ct)
    {
        var personId = new PersonDocumentPersonId(request.PersonId);
        var documentTypeId = new PersonDocumentDocumentTypeId(request.DocumentTypeId);
        var documentNumber = new PersonDocumentNumber(request.DocumentNumber);
        var isPrimary = new PersonDocumentIsPrimary(request.IsPrimary);

        if (await _personDocuments.ExistsDocumentAsync(documentTypeId, documentNumber, ct))
        {
            throw new InvalidOperationException("Ya existe un documento con ese tipo y número.");
        }

        var personDocument = new PersonDocument
        {
            PersonId = personId.Value,
            DocumentTypeId = documentTypeId.Value,
            DocumentNumber = documentNumber.Value,
            IsPrimary = isPrimary.Value
        };

        await _personDocuments.AddAsync(personDocument, ct);
        await _unitOfWork.CommitAsync(ct);

        return personDocument.Id;
    }
}
