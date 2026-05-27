using Application.Abstractions;
using Domain.ValueObjects.Vehicle;
using MediatR;

namespace Application.UseCase.Vehicles;

public sealed class UpdateVehicleHandler : IRequestHandler<UpdateVehicle>
{
    private readonly IVehicleRepository _vehicles;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVehicleHandler(IVehicleRepository vehicles, IUnitOfWork unitOfWork)
    {
        _vehicles = vehicles;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateVehicle request, CancellationToken ct)
    {
        var vehicle = await _vehicles.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el vehículo.");

        var modelId = new VehicleModelId(request.ModelId);
        var vehicleTypeId = new VehicleTypeId(request.VehicleTypeId);
        var vin = new VehicleVin(request.Vin);
        var year = new VehicleYear(request.Year);
        var color = new VehicleColor(request.Color);
        var mileage = new VehicleMileage(request.Mileage);

        vehicle.ModelId = modelId.Value;
        vehicle.VehicleTypeId = vehicleTypeId.Value;
        vehicle.Vin = vin.Value;
        vehicle.Year = year.Value;
        vehicle.Color = color.Value;
        vehicle.Mileage = mileage.Value;
        vehicle.IsActive = request.IsActive;

        await _vehicles.UpdateAsync(vehicle, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
