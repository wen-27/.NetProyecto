using MediatR;

namespace Application.UseCase.PartCategories;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreatePartCategory.
public sealed record CreatePartCategory(string Name) : IRequest<int>;
