using MediatR;

namespace Application.UseCase.Persons;

public sealed record UpdatePerson(int Id, string FirstNames, string LastNames) : IRequest;
