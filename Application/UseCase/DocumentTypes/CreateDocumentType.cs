using MediatR;

namespace Application.UseCase.DocumentTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateDocumentType.
public sealed record CreateDocumentType(string Code, string Name) : IRequest<int>;
