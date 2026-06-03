// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con RecordServiceOrderWorkHandler. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using Application.Abstractions;
using Domain.Enums.OrderStatus;
using Domain.ValueObjects.ServiceOrder;
using MediatR;

namespace Application.UseCase.ServiceOrders;

public sealed class RecordServiceOrderWorkHandler : IRequestHandler<RecordServiceOrderWork>
{
    private readonly IServiceOrderRepository _serviceOrders;
    private readonly IUnitOfWork _unitOfWork;

    public RecordServiceOrderWorkHandler(IServiceOrderRepository serviceOrders, IUnitOfWork unitOfWork)
    {
        _serviceOrders = serviceOrders;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RecordServiceOrderWork request, CancellationToken ct)
    {
        var serviceOrder = await _serviceOrders.GetByIdAsync(request.ServiceOrderId, ct)
            ?? throw new KeyNotFoundException("No se encontró la orden de servicio.");

        if (serviceOrder.OrderStatusId is (int)ServiceOrderStatus.Completed or (int)ServiceOrderStatus.Cancelled)
        {
            throw new InvalidOperationException("No se puede registrar trabajo en una orden completada o cancelada.");
        }

        var workPerformed = new ServiceOrderWorkPerformed(request.WorkPerformed);
        serviceOrder.WorkPerformed = workPerformed.Value;

        await _serviceOrders.UpdateAsync(serviceOrder, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
