using Application.Abstractions;
using Domain.ValueObjects.PersonDocument;
using MediatR;

namespace Application.UseCase.PersonDocuments;

public sealed class UpdatePersonDocumentHandler : IRequestHandler<UpdatePersonDocument>
{
    private readonly IPersonDocumentRepository _personDocuments;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePersonDocumentHandler(IPersonDocumentRepository personDocuments, IUnitOfWork unitOfWork)
    {
        _personDocuments = personDocuments;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdatePersonDocument request, CancellationToken ct)
    {
        var personDocument = await _personDocuments.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el documento de la persona.");

        var documentTypeId = new PersonDocumentDocumentTypeId(request.DocumentTypeId);
        var documentNumber = new PersonDocumentNumber(request.DocumentNumber);
        var isPrimary = new PersonDocumentIsPrimary(request.IsPrimary);

        personDocument.DocumentTypeId = documentTypeId.Value;
        personDocument.DocumentNumber = documentNumber.Value;
        personDocument.IsPrimary = isPrimary.Value;

        await _personDocuments.UpdateAsync(personDocument, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
