using MediatR;

namespace Application.UseCase.Persons;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreatePerson.
public sealed record CreatePerson(string FirstNames, string LastNames) : IRequest<int>;
