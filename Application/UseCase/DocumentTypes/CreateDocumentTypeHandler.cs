// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreateDocumentTypeHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.DocumentType;
using MediatR;

namespace Application.UseCase.DocumentTypes;

public sealed class CreateDocumentTypeHandler : IRequestHandler<CreateDocumentType, int>
{
    private readonly IDocumentTypeRepository _documentTypes;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDocumentTypeHandler(IDocumentTypeRepository documentTypes, IUnitOfWork unitOfWork)
    {
        _documentTypes = documentTypes;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateDocumentType request, CancellationToken ct)
    {
        var code = new DocumentTypeCode(request.Code);
        var name = new DocumentTypeName(request.Name);

        if (await _documentTypes.ExistsCodeAsync(code, ct))
        {
            throw new InvalidOperationException("Ya existe un tipo de documento con ese código.");
        }

        if (await _documentTypes.ExistsNameAsync(name, ct))
        {
            throw new InvalidOperationException("Ya existe un tipo de documento con ese nombre.");
        }

        var documentType = new DocumentType
        {
            Code = code.Value,
            Name = name.Value
        };

        await _documentTypes.AddAsync(documentType, ct);
        await _unitOfWork.CommitAsync(ct);

        return documentType.Id;
    }
}
