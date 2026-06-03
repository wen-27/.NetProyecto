// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreatePersonHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.Person;
using MediatR;

namespace Application.UseCase.Persons;

public sealed class CreatePersonHandler : IRequestHandler<CreatePerson, int>
{
    private readonly IPersonRepository _persons;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePersonHandler(IPersonRepository persons, IUnitOfWork unitOfWork)
    {
        _persons = persons;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreatePerson request, CancellationToken ct)
    {
        var firstNames = new PersonFirstNames(request.FirstNames);
        var lastNames = new PersonLastNames(request.LastNames);
        var registrationDate = new PersonRegistrationDate(DateTime.UtcNow);

        var person = new Person
        {
            FirstNames = firstNames.Value,
            LastNames = lastNames.Value,
            RegistrationDate = registrationDate.Value
        };

        await _persons.AddAsync(person, ct);
        await _unitOfWork.CommitAsync(ct);

        return person.Id;
    }
}
