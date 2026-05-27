using Application.Abstractions;
using Domain.ValueObjects.ServiceOrderService;
using MediatR;

namespace Application.UseCase.ServiceOrderServices;

public sealed class UpdateServiceOrderServiceHandler : IRequestHandler<UpdateServiceOrderService>
{
    private readonly IServiceOrderServiceRepository _services;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateServiceOrderServiceHandler(IServiceOrderServiceRepository services, IUnitOfWork unitOfWork)
    {
        _services = services;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateServiceOrderService request, CancellationToken ct)
    {
        var service = await _services.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("No se encontró el servicio de la orden.");

        var serviceTypeId = new ServiceOrderServiceTypeId(request.ServiceTypeId);
        var mechanicId = new ServiceOrderServiceMechanicId(request.MechanicId);
        var description = new ServiceOrderServiceDescription(request.Description);
        var laborCost = new ServiceOrderServiceLaborCost(request.LaborCost);

        service.ServiceTypeId = serviceTypeId.Value;
        service.MechanicId = mechanicId.Value;
        service.Description = description.Value;
        service.LaborCost = laborCost.Value;

        await _services.UpdateAsync(service, ct);
        await _unitOfWork.CommitAsync(ct);
    }
}
