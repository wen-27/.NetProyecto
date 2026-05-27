using MediatR;

namespace Application.UseCase.Persons;

public sealed record DeletePerson(int Id) : IRequest;
