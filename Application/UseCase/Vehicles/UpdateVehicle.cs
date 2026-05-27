using MediatR;

namespace Application.UseCase.Vehicles;

public sealed record UpdateVehicle(int Id, int ModelId, string Vin, int Year, int Mileage) : IRequest;
