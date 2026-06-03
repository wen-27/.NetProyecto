using MediatR;

namespace Application.UseCase.Persons;

// Caso de uso que modela una accion o consulta de negocio relacionada con DeletePerson.
public sealed record DeletePerson(int Id) : IRequest;
