using MediatR;

namespace Application.UseCase.ServiceOrders;

// Caso de uso que modela una accion o consulta de negocio relacionada con RecordServiceOrderWork.
public sealed record RecordServiceOrderWork(int ServiceOrderId, string WorkPerformed) : IRequest;
