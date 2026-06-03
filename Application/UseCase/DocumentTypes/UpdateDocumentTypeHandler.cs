using Application.Abstractions;
using Domain.ValueObjects.DocumentType;
using MediatR;

namespace Application.UseCase.DocumentTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateDocumentType.
public sealed class UpdateDocumentTypeHandler : IRequestHandler<UpdateDocumentType>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IDocumentTypeRepository _documentTypes;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDocumentTypeHandler(IDocumentTypeRepository documentTypes, IUnitOfWork unitOfWork)
    {
        _documentTypes = documentTypes;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateDocumentType request, CancellationToken ct)
    {
        var documentType = await _documentTypes.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el tipo de documento.");

        var code = new DocumentTypeCode(request.Code);
        var name = new DocumentTypeName(request.Name);

        documentType.Code = code.Value;
        documentType.Name = name.Value;

        await _documentTypes.UpdateAsync(documentType, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
