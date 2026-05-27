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
