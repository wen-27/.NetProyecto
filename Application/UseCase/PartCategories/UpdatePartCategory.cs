using MediatR;

namespace Application.UseCase.PartCategories;

public sealed record UpdatePartCategory(int Id, string Name) : IRequest;
