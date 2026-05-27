using Application.Abstractions;
using Domain.ValueObjects.VehicleBrand;
using MediatR;

namespace Application.UseCase.VehicleBrands;

public sealed class UpdateVehicleBrandHandler : IRequestHandler<UpdateVehicleBrand>
{
    private readonly IVehicleBrandRepository _vehicleBrands;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVehicleBrandHandler(IVehicleBrandRepository vehicleBrands, IUnitOfWork unitOfWork)
    {
        _vehicleBrands = vehicleBrands;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateVehicleBrand request, CancellationToken ct)
    {
        var vehicleBrand = await _vehicleBrands.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró la marca de vehículo.");

        var brandName = new VehicleBrandName(request.BrandName);
        vehicleBrand.BrandName = brandName.Value;

        await _vehicleBrands.UpdateAsync(vehicleBrand, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
