// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreateMechanicAssignmentHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.UseCase.MechanicAssignments;

public sealed class CreateMechanicAssignmentHandler : IRequestHandler<CreateMechanicAssignment, int>
{
    private readonly IMechanicAssignmentRepository _assignments;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMechanicAssignmentHandler(IMechanicAssignmentRepository assignments, IUnitOfWork unitOfWork)
    {
        _assignments = assignments;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateMechanicAssignment request, CancellationToken ct)
    {
        if (await _assignments.HasActiveAssignmentAsync(request.MechanicPersonId, request.OrderServiceId, ct))
        {
            throw new InvalidOperationException("El mecánico ya está asignado a una orden activa.");
        }

        var assignment = new MechanicAssignment
        {
            OrderServiceId = request.OrderServiceId,
            MechanicPersonId = request.MechanicPersonId,
            SpecialtyId = request.SpecialtyId
        };

        await _assignments.AddAsync(assignment, ct);
        await _unitOfWork.CommitAsync(ct);

        return assignment.Id;
    }
}
