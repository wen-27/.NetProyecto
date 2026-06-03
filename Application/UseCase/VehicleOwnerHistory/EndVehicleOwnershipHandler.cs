// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con EndVehicleOwnershipHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.ValueObjects.VehicleOwnerHistory;
using MediatR;

namespace Application.UseCase.VehicleOwnerHistory;

public sealed class EndVehicleOwnershipHandler : IRequestHandler<EndVehicleOwnership>
{
    private readonly IVehicleOwnerHistoryRepository _vehicleOwnerHistory;
    private readonly IUnitOfWork _unitOfWork;

    public EndVehicleOwnershipHandler(IVehicleOwnerHistoryRepository vehicleOwnerHistory, IUnitOfWork unitOfWork)
    {
        _vehicleOwnerHistory = vehicleOwnerHistory;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(EndVehicleOwnership request, CancellationToken ct)
    {
        var vehicleId = new VehicleOwnerHistoryVehicleId(request.VehicleId);
        var endDate = new VehicleOwnerHistoryEndDate(request.EndDate);
        var currentOwner = await _vehicleOwnerHistory.GetCurrentByVehicleIdAsync(vehicleId, ct)
            ?? throw new KeyNotFoundException("No se encontró un propietario vigente para el vehículo.");

        currentOwner.EndDate = endDate.Value;

        await _vehicleOwnerHistory.UpdateAsync(currentOwner, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
