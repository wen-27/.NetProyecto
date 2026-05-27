using MediatR;

namespace Application.UseCase.Vehicles;

public sealed record DeleteVehicle(int Id) : IRequest;
