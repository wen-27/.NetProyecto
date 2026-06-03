// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con UpdateDocumentTypeHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.ValueObjects.DocumentType;
using MediatR;

namespace Application.UseCase.DocumentTypes;

public sealed class UpdateDocumentTypeHandler : IRequestHandler<UpdateDocumentType>
{
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
