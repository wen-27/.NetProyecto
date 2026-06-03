using Application.Abstractions;
using Domain.ValueObjects.Person;
using MediatR;

namespace Application.UseCase.Persons;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdatePerson.
public sealed class UpdatePersonHandler : IRequestHandler<UpdatePerson>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
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
