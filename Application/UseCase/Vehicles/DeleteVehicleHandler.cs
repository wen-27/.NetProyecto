using Application.Abstractions;
using Domain.ValueObjects.ServiceOrder;
using MediatR;

namespace Application.UseCase.Vehicles;

// Caso de uso que modela una accion o consulta de negocio relacionada con DeleteVehicle.
public sealed class DeleteVehicleHandler : IRequestHandler<DeleteVehicle>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    private readonly IServiceOrderRepository _serviceOrders;
    private readonly IVehicleRepository _vehicles;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVehicleHandler(IVehicleRepository vehicles, IServiceOrderRepository serviceOrders, IUnitOfWork unitOfWork)
    {
        _vehicles = vehicles;
        _serviceOrders = serviceOrders;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteVehicle request, CancellationToken ct)
    {
        var vehicle = await _vehicles.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el vehículo.");

        if (await _serviceOrders.HasActiveOrderForVehicleAsync(new ServiceOrderVehicleId(request.Id), ct))
        {
            throw new InvalidOperationException("No se puede eliminar un vehículo con órdenes de servicio activas.");
        }

        await _vehicles.RemoveAsync(vehicle, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
