using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.AuditActionType;
using MediatR;

namespace Application.UseCase.AuditActionTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateAuditActionType.
public sealed class CreateAuditActionTypeHandler : IRequestHandler<CreateAuditActionType, int>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IAuditActionTypeRepository _auditActionTypes;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuditActionTypeHandler(IAuditActionTypeRepository auditActionTypes, IUnitOfWork unitOfWork)
    {
        _auditActionTypes = auditActionTypes;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateAuditActionType request, CancellationToken ct)
    {
        var name = new AuditActionTypeName(request.Name);

        if (await _auditActionTypes.ExistsNameAsync(name, ct))
        {
            throw new InvalidOperationException("Ya existe un tipo de acción de auditoría con ese nombre.");
        }

        var auditActionType = new AuditActionType { Name = name.Value };

        await _auditActionTypes.AddAsync(auditActionType, ct);
        await _unitOfWork.CommitAsync(ct);

        return auditActionType.Id;
    }
}
