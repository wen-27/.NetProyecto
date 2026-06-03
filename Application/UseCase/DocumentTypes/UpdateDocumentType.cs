using MediatR;

namespace Application.UseCase.DocumentTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateDocumentType.
public sealed record UpdateDocumentType(int Id, string Code, string Name) : IRequest;
