using MediatR;

namespace Application.UseCase.DocumentTypes;

public sealed record CreateDocumentType(string Code, string Name) : IRequest<int>;
