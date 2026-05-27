using MediatR;

namespace Application.UseCase.ServiceOrders;

public sealed record RecordServiceOrderWork(int ServiceOrderId, string WorkPerformed) : IRequest;
