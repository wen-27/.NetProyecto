using MediatR;

namespace Application.UseCase.DocumentTypes;

public sealed record UpdateDocumentType(int Id, string Code, string Name) : IRequest;
