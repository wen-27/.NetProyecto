// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con UpdateAuditActionTypeHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.ValueObjects.AuditActionType;
using MediatR;

namespace Application.UseCase.AuditActionTypes;

public sealed class UpdateAuditActionTypeHandler : IRequestHandler<UpdateAuditActionType>
{
    private readonly IAuditActionTypeRepository _auditActionTypes;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAuditActionTypeHandler(IAuditActionTypeRepository auditActionTypes, IUnitOfWork unitOfWork)
    {
        _auditActionTypes = auditActionTypes;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateAuditActionType request, CancellationToken ct)
    {
        var auditActionType = await _auditActionTypes.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el tipo de acción de auditoría.");

        var name = new AuditActionTypeName(request.Name);
        auditActionType.Name = name.Value;

        await _auditActionTypes.UpdateAsync(auditActionType, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
