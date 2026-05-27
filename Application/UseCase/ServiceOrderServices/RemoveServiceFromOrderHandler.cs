using Application.Abstractions;
using MediatR;

namespace Application.UseCase.ServiceOrderServices;

public sealed class RemoveServiceFromOrderHandler : IRequestHandler<RemoveServiceFromOrder>
{
    private readonly IServiceOrderServiceRepository _services;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveServiceFromOrderHandler(IServiceOrderServiceRepository services, IUnitOfWork unitOfWork)
    {
        _services = services;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveServiceFromOrder request, CancellationToken ct)
    {
        var service = await _services.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el servicio de la orden.");

        await _services.RemoveAsync(service, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
