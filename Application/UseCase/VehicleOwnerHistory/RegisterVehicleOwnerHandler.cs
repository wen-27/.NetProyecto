using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects.VehicleOwnerHistory;
using MediatR;

namespace Application.UseCase.VehicleOwnerHistory;

// Caso de uso que modela una accion o consulta de negocio relacionada con RegisterVehicleOwner.
public sealed class RegisterVehicleOwnerHandler : IRequestHandler<RegisterVehicleOwner, int>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IVehicleOwnerHistoryRepository _vehicleOwnerHistory;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterVehicleOwnerHandler(IVehicleOwnerHistoryRepository vehicleOwnerHistory, IUnitOfWork unitOfWork)
    {
        _vehicleOwnerHistory = vehicleOwnerHistory;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(RegisterVehicleOwner request, CancellationToken ct)
    {
        var vehicleId = new VehicleOwnerHistoryVehicleId(request.VehicleId);
        var personId = new VehicleOwnerHistoryPersonId(request.PersonId);
        var startDate = new VehicleOwnerHistoryStartDate(request.StartDate);

        var currentOwner = await _vehicleOwnerHistory.GetCurrentByVehicleIdAsync(vehicleId, ct);
        if (currentOwner is not null)
        {
            currentOwner.EndDate = startDate.Value;
            await _vehicleOwnerHistory.UpdateAsync(currentOwner, ct);
        }

        var ownerHistory = new Domain.Entities.VehicleOwnerHistory
        {
            VehicleId = vehicleId.Value,
            PersonId = personId.Value,
            StartDate = startDate.Value
        };

        await _vehicleOwnerHistory.AddAsync(ownerHistory, ct);
        await _unitOfWork.CommitAsync(ct);

        return ownerHistory.Id;
    }
}
