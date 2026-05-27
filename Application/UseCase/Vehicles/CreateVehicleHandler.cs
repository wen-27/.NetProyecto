using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.Vehicle;
using MediatR;

namespace Application.UseCase.Vehicles;

public sealed class CreateVehicleHandler : IRequestHandler<CreateVehicle, int>
{
    private readonly IVehicleRepository _vehicles;
    private readonly IUnitOfWork _unitOfWork;

    public CreateVehicleHandler(IVehicleRepository vehicles, IUnitOfWork unitOfWork)
    {
        _vehicles = vehicles;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateVehicle request, CancellationToken ct)
    {
        var modelId = new VehicleModelId(request.ModelId);
        var vehicleTypeId = new VehicleTypeId(request.VehicleTypeId);
        var vin = new VehicleVin(request.Vin);
        var year = new VehicleYear(request.Year);
        var color = new VehicleColor(request.Color);
        var mileage = new VehicleMileage(request.Mileage);

        if (await _vehicles.ExistsVinAsync(vin, ct))
        {
            throw new InvalidOperationException("Ya existe un vehículo con ese VIN.");
        }

        var vehicle = new Vehicle
        {
            ModelId = modelId.Value,
            VehicleTypeId = vehicleTypeId.Value,
            Vin = vin.Value,
            Year = year.Value,
            Color = color.Value,
            Mileage = mileage.Value,
            IsActive = request.IsActive
        };

        await _vehicles.AddAsync(vehicle, ct);
        await _unitOfWork.CommitAsync(ct);

        return vehicle.Id;
    }
}
