using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.VehicleBrand;
using MediatR;

namespace Application.UseCase.VehicleBrands;

public sealed class CreateVehicleBrandHandler : IRequestHandler<CreateVehicleBrand, int>
{
    private readonly IVehicleBrandRepository _vehicleBrands;
    private readonly IUnitOfWork _unitOfWork;

    public CreateVehicleBrandHandler(IVehicleBrandRepository vehicleBrands, IUnitOfWork unitOfWork)
    {
        _vehicleBrands = vehicleBrands;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateVehicleBrand request, CancellationToken ct)
    {
        var brandName = new VehicleBrandName(request.BrandName);

        if (await _vehicleBrands.ExistsNameAsync(brandName, ct))
        {
            throw new InvalidOperationException("Ya existe una marca de vehículo con ese nombre.");
        }

        var vehicleBrand = new VehicleBrand { BrandName = brandName.Value };

        await _vehicleBrands.AddAsync(vehicleBrand, ct);
        await _unitOfWork.CommitAsync(ct);

        return vehicleBrand.Id;
    }
}
