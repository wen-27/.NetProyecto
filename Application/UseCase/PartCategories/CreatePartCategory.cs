using MediatR;

namespace Application.UseCase.PartCategories;

public sealed record CreatePartCategory(string Name) : IRequest<int>;
