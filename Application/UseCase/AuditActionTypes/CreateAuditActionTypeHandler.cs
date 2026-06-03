// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreateAuditActionTypeHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.AuditActionType;
using MediatR;

namespace Application.UseCase.AuditActionTypes;

public sealed class CreateAuditActionTypeHandler : IRequestHandler<CreateAuditActionType, int>
{
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
