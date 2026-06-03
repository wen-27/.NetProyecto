// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con UpdatePersonHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.ValueObjects.Person;
using MediatR;

namespace Application.UseCase.Persons;

public sealed class UpdatePersonHandler : IRequestHandler<UpdatePerson>
{
    private readonly IPersonRepository _persons;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePersonHandler(IPersonRepository persons, IUnitOfWork unitOfWork)
    {
        _persons = persons;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdatePerson request, CancellationToken ct)
    {
        var person = await _persons.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró la persona.");

        var firstNames = new PersonFirstNames(request.FirstNames);
        var lastNames = new PersonLastNames(request.LastNames);

        person.FirstNames = firstNames.Value;
        person.LastNames = lastNames.Value;

        await _persons.UpdateAsync(person, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
