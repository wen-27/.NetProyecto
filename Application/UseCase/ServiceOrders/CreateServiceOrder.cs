// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreateServiceOrder. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using MediatR;

namespace Application.UseCase.ServiceOrders;

public sealed record CreateServiceOrder(
    int VehicleId,
    int OrderStatusId,
    DateTime? EstimatedDeliveryDate) : IRequest<int>;
