using MediatR;

namespace Application.UseCase.Persons;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdatePerson.
public sealed record UpdatePerson(int Id, string FirstNames, string LastNames) : IRequest;
