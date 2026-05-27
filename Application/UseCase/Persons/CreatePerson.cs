using MediatR;

namespace Application.UseCase.Persons;

public sealed record CreatePerson(string FirstNames, string LastNames) : IRequest<int>;
