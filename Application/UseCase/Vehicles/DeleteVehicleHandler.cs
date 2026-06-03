// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con DeleteVehicleHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.ValueObjects.ServiceOrder;
using MediatR;

namespace Application.UseCase.Vehicles;

public sealed class DeleteVehicleHandler : IRequestHandler<DeleteVehicle>
{
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
