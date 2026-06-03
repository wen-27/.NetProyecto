using MediatR;

namespace Application.UseCase.PartCategories;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdatePartCategory.
public sealed record UpdatePartCategory(int Id, string Name) : IRequest;
