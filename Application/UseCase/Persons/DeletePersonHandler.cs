using Application.Abstractions;
using Application.Common.Exceptions;
using MediatR;

namespace Application.UseCase.Persons;

public sealed class DeletePersonHandler : IRequestHandler<DeletePerson>
{
    private readonly IPersonRepository _persons;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePersonHandler(IPersonRepository persons, IUnitOfWork unitOfWork)
    {
        _persons = persons;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeletePerson request, CancellationToken ct)
    {
        var person = await _persons.GetByIdAsync(request.Id, ct)
            ?? throw new NotFoundException("Persona", request.Id);

        if (await _persons.HasActiveServiceOrdersAsCurrentOwnerAsync(request.Id, ct))
        {
            throw new ConflictException("No se puede eliminar una persona con vehículos asociados a órdenes de servicio activas.");
        }

        if (await _persons.HasActiveServiceOrdersAsMechanicAsync(request.Id, ct))
        {
            throw new ConflictException("No se puede eliminar una persona asignada como mecánico en órdenes de servicio activas.");
        }

        await _persons.RemoveAsync(person, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
