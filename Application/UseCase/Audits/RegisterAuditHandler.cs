// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con RegisterAuditHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.Audit;
using MediatR;

namespace Application.UseCase.Audits;

public sealed class RegisterAuditHandler : IRequestHandler<RegisterAudit, int>
{
    private readonly IAuditRepository _audits;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterAuditHandler(IAuditRepository audits, IUnitOfWork unitOfWork)
    {
        _audits = audits;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(RegisterAudit request, CancellationToken ct)
    {
        var userId = new AuditUserId(request.UserId);
        var actionTypeId = new AuditActionTypeId(request.AuditActionTypeId);
        var affectedEntity = new AuditAffectedEntity(request.AffectedEntity);
        var affectedRecordId = new AuditAffectedRecordId(request.AffectedRecordId);
        var description = new AuditDescription(request.Description);

        var audit = new Audit
        {
            UserId = userId.Value,
            AuditActionTypeId = actionTypeId.Value,
            AffectedEntity = affectedEntity.Value,
            AffectedRecordId = affectedRecordId.Value,
            Description = description.Value
        };

        await _audits.AddAsync(audit, ct);
        await _unitOfWork.CommitAsync(ct);

        return audit.Id;
    }
}
