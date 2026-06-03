// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreateVehicleModelHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.VehicleModel;
using MediatR;

namespace Application.UseCase.VehicleModels;

public sealed class CreateVehicleModelHandler : IRequestHandler<CreateVehicleModel, int>
{
    private readonly IVehicleModelRepository _vehicleModels;
    private readonly IUnitOfWork _unitOfWork;

    public CreateVehicleModelHandler(IVehicleModelRepository vehicleModels, IUnitOfWork unitOfWork)
    {
        _vehicleModels = vehicleModels;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateVehicleModel request, CancellationToken ct)
    {
        var brandId = new VehicleModelBrandId(request.BrandId);
        var modelName = new VehicleModelName(request.ModelName);

        if (await _vehicleModels.ExistsBrandAndNameAsync(brandId, modelName, ct))
        {
            throw new InvalidOperationException("Ya existe ese modelo para la marca indicada.");
        }

        var vehicleModel = new VehicleModel
        {
            BrandId = brandId.Value,
            ModelName = modelName.Value
        };

        await _vehicleModels.AddAsync(vehicleModel, ct);
        await _unitOfWork.CommitAsync(ct);

        return vehicleModel.Id;
    }
}
