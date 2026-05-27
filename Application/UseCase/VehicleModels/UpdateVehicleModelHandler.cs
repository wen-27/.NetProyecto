using Application.Abstractions;
using Domain.ValueObjects.VehicleModel;
using MediatR;

namespace Application.UseCase.VehicleModels;

public sealed class UpdateVehicleModelHandler : IRequestHandler<UpdateVehicleModel>
{
    private readonly IVehicleModelRepository _vehicleModels;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVehicleModelHandler(IVehicleModelRepository vehicleModels, IUnitOfWork unitOfWork)
    {
        _vehicleModels = vehicleModels;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateVehicleModel request, CancellationToken ct)
    {
        var vehicleModel = await _vehicleModels.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el modelo de vehículo.");

        var brandId = new VehicleModelBrandId(request.BrandId);
        var modelName = new VehicleModelName(request.ModelName);

        vehicleModel.BrandId = brandId.Value;
        vehicleModel.ModelName = modelName.Value;

        await _vehicleModels.UpdateAsync(vehicleModel, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
